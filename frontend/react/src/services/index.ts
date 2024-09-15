import { AuthService } from "@/services/auth.service";
import { TokenService } from "@/services/token.service";

// TODO: Make this configurable
const accessApiUrl = 'http://localhost:3000';

export const authService = new AuthService(accessApiUrl);
export const tokenService = new TokenService(accessApiUrl);
