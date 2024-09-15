import { AxiosInstance } from "axios";
import { cookies } from "next/headers";
import { authService, tokenService } from "@/services";
import { AuthToken } from "@/types/auth-token";
import { User } from "@/types/user";

// TODO: Consolidate auth token and user cookies?

export const authTokenKey = 'authToken';
export const userTokenKey = 'user';

export function getAuthToken(): AuthToken | undefined {
  const cookieValue = cookies().get(authTokenKey)?.value;
  return cookieValue ? JSON.parse(cookieValue) as AuthToken : undefined;
}

export async function setAuthTokenAsync(authToken: AuthToken, updateUserToken: boolean = true): Promise<void> {
  cookies().set(authTokenKey, JSON.stringify(authToken), {
    httpOnly: true,
    secure: true,
    expires: authToken.expirationDate,
    // TODO: Are the 'sameSite' and 'path' options required?
    sameSite: 'none',
    path: '/',
  });
  if (updateUserToken) {
    await setUserTokenAsync();
  }
}

export function deleteAuthToken(): void {
  cookies().delete(authTokenKey);
  deleteUserToken();
}

export function getUserToken(): User | undefined {
  const cookieValue = cookies().get(userTokenKey)?.value;
  return cookieValue ? JSON.parse(cookieValue) as User : undefined;
}

export async function setUserTokenAsync(): Promise<void> {
  const user = await authService.getCurrentUserAsync();
  cookies().set(userTokenKey, JSON.stringify(user), {
    httpOnly: true,
    secure: true,
  });
}

export function deleteUserToken(): void {
  cookies().delete(userTokenKey);
}

export function addAuthTokenInjectionInterceptor(instance: AxiosInstance) {
  instance.interceptors.request.use(request => {
    const authToken = getAuthToken();
    if (authToken) {
      request.headers['Authorization'] = authToken.accessToken;
    }
    return request;
  }, error => {
    return Promise.reject(error);
  });
}

/**
 * Adds a response interceptor that retries requests that fail with status code 401 with a refreshed auth token.
 * @param instance The AxiosInstance to which to add the interceptor.
 */
export function addAuthTokenRefreshInterceptor(instance: AxiosInstance) {
  instance.interceptors.response.use(
    response => response, // Directly return successful responses.
    async error => {
      const originalRequest = error.config;
      if (error.response.status === 401 && !originalRequest._retry) {
        originalRequest._retry = true; // Mark the request as retried to avoid infinite loops.

        // Check to see if the user is authenticated
        const authToken = await tokenService.refreshTokenAsync();
        if (!authToken) {
          // TODO: Redirect to login page
          return Promise.reject(error);
        }

        // Refresh the auth token
        try {
          // Update the authorization header with the new access token.
          instance.defaults.headers.common['Authorization'] = `Bearer ${authToken}`;
          // Retry the original request with the new access token.
          // TODO: Update authorization header via request options? i.e. { headers: { Authorization: `Bearer ${authToken.accessToken}` } }
          return instance(originalRequest);
        } catch (refreshError) {
          // Handle refresh token errors by clearing stored tokens and redirecting to the login page.
          console.error('Token refresh failed:', refreshError);
          deleteAuthToken();
          // TODO: Redirect to login page (window.location.href = '/login';?)
          return Promise.reject(refreshError);
        }
      }
      return Promise.reject(error); // For all other errors, return the error as is.
    }
  );
}
