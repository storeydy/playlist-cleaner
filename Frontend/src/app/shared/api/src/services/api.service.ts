import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})

export class ApiService {

  private http = inject(HttpClient);
  private readonly basePath = 'https://localhost:7204';


  get<T>(path: string) {
		return this.http.get<T>(`${this.basePath}${path}`);
	}

  getBlob(path: string) {
		return this.http.get(`${this.basePath}${path}`, {
			responseType: 'blob',
		});
	}

  put<T>(path: string, body: object) {
		return this.http.put<T>(`${this.basePath}${path}`, body);
	}

  patch<T>(path: string, body: object) {
		return this.http.patch<T>(`${this.basePath}${path}`, body);
	}

  post<T>(path: string, body: object) {
		return this.http.post<T>(`${this.basePath}${path}`, body);
	}

  delete<T>(path: string) {
		return this.http.delete<T>(`${this.basePath}${path}`);
	}
}
