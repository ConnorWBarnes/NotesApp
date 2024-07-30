import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { firstValueFrom, Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { Response } from './response';
import { UserClaim } from './user-claim';
import { UserProfile } from './user-profile';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  // TODO: Refactor url, headers, and HttpClient into base class?
  private readonly authenticationUrl = 'http://localhost:3000/auth';

  private readonly httpHeaders = new HttpHeaders(
    {
      'Content-Type': 'application/json',
    }
  );

  constructor(private http: HttpClient) { }

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

  public logIn$(email: string, password: string, rememberMe: boolean): Observable<HttpResponse<string>> {
    return this.http.post<HttpResponse<string>>(this.authenticationUrl, { email: email, password: password, rememberMe: rememberMe }, { headers: this.httpHeaders }).pipe(
      tap((response: HttpResponse<string>) => this._isLoggedIn = response.ok),
      catchError(this.handleError<HttpResponse<string>>('logIn$'))
    );
  }

  public async logInAsync(email: string, password: string, rememberMe: boolean): Promise<Response> {
    const response = await firstValueFrom(this.http.post(this.authenticationUrl, { email: email, password: password, rememberMe: rememberMe }, { headers: this.httpHeaders, observe: 'response' }));
    this._isLoggedIn = response.ok;
    return { isSuccess: response.ok, message: response.body?.toString() };
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
    return this.http.get<UserClaim[]>(this.authenticationUrl, { withCredentials: true });
  }

  public async getUserAsync(): Promise<UserClaim[]> {
    return await firstValueFrom(this.http.get<UserClaim[]>(this.authenticationUrl, { withCredentials: true }));
  }
  
  public getUserProfile$(): Observable<UserProfile> {
    return this.http.get<UserProfile>(this.authenticationUrl, { withCredentials: true });
  }

  public async getUserProfileAsync(): Promise<UserProfile> {
    const profile = await firstValueFrom(this.http.get<UserProfile>(this.authenticationUrl, { withCredentials: true }));
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
    const labeledMessage = `NoteService: ${message}`;
    console.log(labeledMessage)
  }
}
