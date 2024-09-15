export interface IAuthToken {
  tokenType: string;
  accessToken: string;
  expiresIn: number;
  refreshToken: string;
}
