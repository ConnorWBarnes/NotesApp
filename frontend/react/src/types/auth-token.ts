import { IAuthToken } from "@/types/i-auth-token";

export class AuthToken {
  tokenType: string;
  accessToken: string;
  expirationDate: Date;
  refreshToken: string;

  constructor(token: IAuthToken) {
    this.tokenType = token.tokenType;
    this.accessToken = token.accessToken;
    this.expirationDate = new Date(Date.now() + (token.expiresIn * 1000));
    this.refreshToken = token.refreshToken;
  }

  public get isExpired(): boolean {
    return new Date() > this.expirationDate;
  }
}
