import axios, { AxiosInstance } from "axios";
import { AuthToken } from "@/types/auth-token";
import { IAuthToken } from "@/types/i-auth-token";
import { User } from "@/types/user";
import { UserClaim } from "@/types/user-claim";
import { addAuthTokenInjectionInterceptor, addAuthTokenRefreshInterceptor, getAuthToken } from "@/utils/auth-utils";

const logSource = 'AuthService';

// TODO: Add logging and error handling
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
    let response = await this.instance.post<IAuthToken>('/login', { email, password });
    return new AuthToken(response.data);
  }

  /**
   * POST: Refreshes the current authentication token.
   * @returns The refreshed authentication token.
   */
  public async refreshTokenAsync(): Promise<AuthToken> {
    const refreshToken = getAuthToken()?.refreshToken ?? '';
    let response = await this.instance.post<IAuthToken>('/refresh', { refreshToken });
    return new AuthToken(response.data);
  }

  /**
   * POST: Registers a new user.
   * @param email The new user's email address.
   * @param password The new user's password.
   * @returns A flag indicating the success of the operation.
   */
  public async registerUserAsync(email: string, password: string): Promise<boolean> {
    let response = await this.instance.post('/register', { email, password });
    return response.status.toString().startsWith('2');
  }

  /**
   * GET: Gets the current user's info.
   * @returns The current user's info.
   */
  public async getCurrentUserAsync(): Promise<User> {
    let response = await this.instance.get<UserClaim[]>('/user');
    // Parse the response
    const userClaims = response.data;
    return {
      id: userClaims.find((claim) => claim.type.endsWith('nameidentifier'))?.value ?? '',
      name: userClaims.find((claim) => claim.type.endsWith('name'))?.value ?? '',
      email: userClaims.find((claim) => claim.type.endsWith('emailaddress'))?.value ?? '',
    };
  }
}
