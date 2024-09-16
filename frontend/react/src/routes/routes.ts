export const loginRoute = '/login';
export const profileRoute = '/profile';

export const protectedRoutes = ['/notes', profileRoute];
export const authRoutes = [loginRoute];
export const publicRoutes = ['/about', '/'];
