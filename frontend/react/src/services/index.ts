import { AuthService } from "@/services/auth.service";

// TODO: Make this configurable
const accessApiUrl = 'http://localhost:3000';

export const authService = new AuthService(accessApiUrl);
