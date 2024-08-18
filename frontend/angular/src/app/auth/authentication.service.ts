import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { firstValueFrom, Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { Response } from './response';
import { UserClaim } from './user-claim';
import { UserProfile } from './user-profile';
import { TokenResponse } from './token-response';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  // TODO: Refactor url, headers, and HttpClient into base class?
  private readonly backendUrl = 'http://localhost:3000';
  private readonly authenticationUrl = `${this.backendUrl}/auth`;
  private readonly loginUrl = `${this.backendUrl}/login`;
  private readonly refreshUrl = `${this.backendUrl}/refresh`;
  private readonly usersUrl = `${this.backendUrl}/users`;
  private readonly userProfilesUrl = `${this.usersUrl}/profiles`;

  private readonly httpHeaders = new HttpHeaders(
    {
      'Content-Type': 'application/json',
    }
  );

  constructor(private http: HttpClient) { }

  private tokenResponse: TokenResponse | null = null;
  private getTokenHeaders() {
    return new HttpHeaders(
      {
        'Authorization': `Bearer ${this.tokenResponse?.accessToken}`
      }
    );
  }

  // Stores the URL that requires authentication so that we can redirect to it after successful authentication.
  private _redirectUrl?: string;
  public get redirectUrl(): string {
    return this._redirectUrl ?? '/';
  }
  public set redirectUrl(url: string | undefined) {
    this._redirectUrl = url;
  }

  private _isLoggedIn = false;
  public get isLoggedIn(): boolean {
    return this._isLoggedIn;
  }

  public logIn$(email: string, password: string, rememberMe: boolean): Observable<TokenResponse> {
    return this.http.post<TokenResponse>(this.loginUrl, { email: email, password: password }, { headers: this.httpHeaders }).pipe(
      tap((response: TokenResponse) => {
        this._isLoggedIn = true;
        this.tokenResponse = response;
      }),
      catchError(this.handleError<TokenResponse>('logIn$'))
    );
  }

  public async logInAsync(email: string, password: string, rememberMe: boolean): Promise<TokenResponse> {
    return firstValueFrom(this.logIn$(email, password, rememberMe));
  }

  public refreshToken$(): Observable<TokenResponse> {
    return this.http.post<TokenResponse>(this.refreshUrl, { refreshToken: this.tokenResponse?.refreshToken }).pipe(
      tap((response: TokenResponse) => {
        this.tokenResponse = response;
      }),
      catchError(this.handleError<TokenResponse>('refreshToken$'))
    );
  }

  public async refreshTokenAsync(): Promise<TokenResponse> {
    return firstValueFrom(this.refreshToken$());
  }

  public logOut$(): Observable<HttpResponse<string>> {
    return this.http.delete<HttpResponse<string>>(this.authenticationUrl).pipe(
      tap(_ => this._isLoggedIn = false),
      catchError(this.handleError<HttpResponse<string>>('logOut$'))
    );
  }

  public async logOutAsync(): Promise<HttpResponse<string>> {
    return await firstValueFrom(this.logOut$());
  }

  public getUser$(): Observable<UserClaim[]> {
    return this.http.get<UserClaim[]>(this.usersUrl, { headers: this.getTokenHeaders(), withCredentials: true }).pipe(
      catchError(this.handleError<UserClaim[]>('getUser$'))
    );
  }

  public async getUserAsync(): Promise<UserClaim[]> {
    return await firstValueFrom(this.http.get<UserClaim[]>(this.usersUrl, { headers: this.getTokenHeaders(), withCredentials: true }));
  }
  
  public getUserProfile$(): Observable<UserProfile> {
    console.log('User profiles URL: ', this.userProfilesUrl);
    return this.http.get<UserProfile>(this.userProfilesUrl, { headers: this.getTokenHeaders(), withCredentials: true }).pipe(
      catchError(this.handleError<UserProfile>('getUserProfile$'))
    );
  }

  public async getUserProfileAsync(): Promise<UserProfile> {
    const profile = await firstValueFrom(this.http.get<UserProfile>(this.userProfilesUrl, { headers: this.getTokenHeaders(), withCredentials: true }));
    console.log('User profile: ', profile);
    return profile;
  }

  /**
   * Handles a failed HTTP operation and allows the app to continue.
   * @param operation The name of the operation that failed.
   * @param result Optional value to return as the observable result.
   */
  private handleError<T>(operation = "operation", result?: T) {
    return (error: any): Observable<T> => {
      // TODO: Send the error to remote logging infrastructure
      console.error(error);

      // TODO: Improve error transformation for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Return an empty result to allow the app to continue running
      return of(result as T);
    }
  }
  
  /**
   * Labels and logs a message.
   * @param message The message to log.
   */
  private log(message: string): void {
    const labeledMessage = `AuthService: ${message}`;
    console.log(labeledMessage)
  }
}
