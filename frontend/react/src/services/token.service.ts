import axios from "axios";
import Semaphore from "ts-semaphore";
import { AuthToken } from "@/types/auth-token";
import { IAuthToken } from "@/types/i-auth-token";
import { getAuthToken, setAuthTokenAsync } from "@/utils/auth-utils";

export class TokenService {
  private readonly semaphore = new Semaphore(1);
  private readonly url: string;
  public constructor(url: string) {
    this.url = url;
  }

  public async refreshTokenAsync(): Promise<AuthToken | undefined> {
    await this.semaphore.aquire();
    try {
      // Check if the token exists and is expired
      let authToken = getAuthToken();
      if (authToken?.isExpired) {
        // Refresh the token
        let response = await axios.post<IAuthToken>(`${this.url}/refresh`, { refreshToken: authToken.refreshToken });
        authToken = new AuthToken(response.data);
        await setAuthTokenAsync(authToken, false);
      }

      return authToken;
    } finally {
      this.semaphore.release();
    }
  }
}
