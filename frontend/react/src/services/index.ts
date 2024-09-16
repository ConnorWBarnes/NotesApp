import { AuthService } from "@/services/auth.service";
import { NoteService } from "@/services/note.service";
import { TokenService } from "@/services/token.service";

// TODO: Make this configurable
const accessApiUrl = 'http://localhost:3000';
const notesApiUrl = 'http://localhost:3001';

export const authService = new AuthService(accessApiUrl);
export const tokenService = new TokenService(accessApiUrl);
export const noteService = new NoteService(notesApiUrl);
