import axios, { AxiosInstance } from "axios";
import { AuthToken } from "@/types/auth-token";
import { IAuthToken } from "@/types/i-auth-token";
import { User } from "@/types/user";
import { UserClaim } from "@/types/user-claim";
import { addAuthTokenInjectionInterceptor, addAuthTokenRefreshInterceptor, getAuthToken } from "@/utils/auth-utils";
import { handleErrorAsync } from "@/utils/logging-utils";

const logSource = 'AuthService';

export class AuthService {
  protected readonly instance: AxiosInstance;

  public constructor(url: string) {
    this.instance = axios.create({
      baseURL: url,
      timeout: 50000,
      timeoutErrorMessage: "Auth request timed out",
    });
    addAuthTokenInjectionInterceptor(this.instance);
    addAuthTokenRefreshInterceptor(this.instance);
  }

  /**
   * POST: Uses the given credentials to log in a user.
   * @param email The user's email.
   * @param password The user's password.
   * @returns The authentication token received from logging in.
   */
  public async loginAsync(email: string, password: string): Promise<AuthToken> {
    try {
      let response = await this.instance.post<IAuthToken>('/login', { email, password });
      return new AuthToken(response.data);
    } catch (error) {
      return handleErrorAsync(logSource, error, 'loginAsync');
    }
  }

  /**
   * POST: Refreshes the current authentication token.
   * @returns The refreshed authentication token.
   */
  public async refreshTokenAsync(): Promise<AuthToken> {
    try {
      const refreshToken = getAuthToken()?.refreshToken ?? '';
      let response = await this.instance.post<IAuthToken>('/refresh', { refreshToken });
      return new AuthToken(response.data);
    } catch (error) {
      return handleErrorAsync(logSource, error, 'refreshTokenAsync');
    }
  }

  /**
   * POST: Registers a new user.
   * @param email The new user's email address.
   * @param password The new user's password.
   * @returns A flag indicating the success of the operation.
   */
  public async registerUserAsync(email: string, password: string): Promise<boolean> {
    try {
      let response = await this.instance.post('/register', { email, password });
      return response.status.toString().startsWith('2');
    } catch (error) {
      return handleErrorAsync(logSource, error, 'registerUserAsync');
    }
  }

  /**
   * GET: Gets the current user's info.
   * @returns The current user's info.
   */
  public async getCurrentUserAsync(): Promise<User> {
    try {
      let response = await this.instance.get<UserClaim[]>('/user');
      // Parse the response
      const userClaims = response.data;
      return {
        id: userClaims.find((claim) => claim.type.endsWith('nameidentifier'))?.value ?? '',
        name: userClaims.find((claim) => claim.type.endsWith('name'))?.value ?? '',
        email: userClaims.find((claim) => claim.type.endsWith('emailaddress'))?.value ?? '',
      };
    } catch (error) {
      return handleErrorAsync(logSource, error, 'getCurrentUserAsync');
    }
  }
}
