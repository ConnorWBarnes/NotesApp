import { AuthToken } from "@/types/auth-token";
import { User } from "@/types/user";
import { UserClaim } from "@/types/user-claim";
import { addQueryToUrl, appendPathToUrl, handleErrorAsync, log } from "@/utils/utils";

const authUrl = 'http://localhost:3000';

const httpHeaders = { 'Content-Type': 'application/json' };

const logSource = 'AuthService';

/**
 * POST: Uses the given credentials to log in a user.
 * @param email The user's email.
 * @param password The user's password.
 * @param useCookies A flag indicating whether to use cookies (optional).
 * @param useSessionCookies A flag indicating whether to use session cookies (optional).
 * @returns The authentication token received from logging in.
 */
export async function loginAsync(email: string, password: string, useCookies: boolean | null = null, useSessionCookies: boolean | null = null): Promise<AuthToken | boolean> {
  let url = appendPathToUrl(authUrl, 'login');
  if (useCookies) {
    url = addQueryToUrl(url, 'useCookies', useCookies.toString());
  }
  if (useSessionCookies) {
    url = addQueryToUrl(url, 'useSessionCookies', useSessionCookies.toString());
  }
  console.log(`URL: ${url} Body: ${JSON.stringify({ email, password })}`);
  return await fetch(url, { cache: 'no-store', method: 'POST', headers: httpHeaders, body: JSON.stringify({ email, password }), })
    .then(async response => {
      if (!response.ok) {
        throw new Error(response.status === 401 ? 'Invalid credentials.' : `Something went wrong. Status code ${response.status}`);
      }
      const body = await response.json();
      console.log(`Response body: ${JSON.stringify(body)}`);
      if (!(useCookies || useSessionCookies)) {
        return await body as AuthToken;
      }
      return response.ok;
    });
}

/**
 * GET: Gets the current user's info.
 * @returns The current user's info.
 */
export async function getUserAsync(accessToken: string): Promise<User> {
  let url = appendPathToUrl(authUrl, 'users');
  return await fetch(url, { cache: 'no-store', method: 'GET', headers: { 'Authorization': `Bearer ${accessToken}`}, })
    .then(async response => {
      if (!response.ok) {
        return handleErrorAsync(logSource, new Error(await response.json()), 'getUserAsync');
      }
      // Parse the response
      const userClaims = await response.json() as UserClaim[];
      const user: User = {
        id: userClaims.find((claim) => claim.type.endsWith('nameidentifier'))?.value ?? '',
        name: userClaims.find((claim) => claim.type.endsWith('name'))?.value ?? '',
        email: userClaims.find((claim) => claim.type.endsWith('emailaddress'))?.value ?? '',
      };
      return user;
    });
}

/**
 * POST: Refreshes an authentication token.
 * @param refreshToken The refresh token of the authentication token to refresh.
 * @returns A refreshed authentication token.
 */
export async function refreshTokenAsync(refreshToken: string): Promise<AuthToken> {
  const url = appendPathToUrl(authUrl, 'refresh');
  return await fetch(url, { cache: 'no-store', method: 'POST', headers: httpHeaders, body: JSON.stringify({ refreshToken }), })
    .then(async response => {
      if (!response.ok) {
        return handleErrorAsync(logSource, new Error(await response.json()), 'refreshTokenAsync');
      }

      log(logSource, `refreshTokenAsync: Successfully refreshed authentication token.`);
      return await response.json() as AuthToken;
    });
}

/**
 * POST: Registers a new user.
 * @param email The new user's email address.
 * @param password The new user's password.
 * @returns A flag indicating the success of the operation.
 */
export async function registerUserAsync(email: string, password: string): Promise<boolean> {
  const url = appendPathToUrl(authUrl, 'register');
  return await fetch(url, { cache: 'no-store', method: 'POST', headers: httpHeaders, body: JSON.stringify({ email, password }), })
    .then(async response => {
      if (!response.ok) {
        return handleErrorAsync(logSource, new Error(await response.json()), 'registerUserAsync');
      }

      log(logSource, `registerUserAsync: Successfully registered user with email: ${email}`);
      return response.ok;
    });
}
