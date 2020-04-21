import { map, filter, scan } from '../../../../node_modules/rxjs/operators';
import { Injectable } from '../../../../node_modules/@angular/core';
import { HttpClient, HttpHeaders } from '../../../../node_modules/@angular/common/http';
import { Observable } from '../../../../node_modules/rxjs';

@Injectable()
export class HttpService {
	get: <T>(baseUrl: string, secondayUrl: string) => Observable<T>;
	post: <T>(baseUrl: string, secondaryUrl: string, data: any) => Observable<T>;
	put: <T>(baseUrl: string, secondaryUrl: string, data: any) => Observable<T>;
	delete: <T>(baseUrl: string, secondaryUrl: string) => Observable<T>;

	constructor(private httpClient: HttpClient) {
		const vm = this;

		vm.get = <T>(baseUrl: string, secondaryUrl: string) => {
			return vm.httpClient.get<T>(baseUrl + secondaryUrl).pipe(map((response: T) => {
				return response;
			}));
		};

		vm.post = <T>(baseUrl: string, secondaryUrl: string, data: any) => {
			return vm.httpClient.post<T>(baseUrl + secondaryUrl, data).pipe(map((response: T) => {
				return response;
			}));
		};

		vm.put = <T>(baseUrl: string, secondayUrl: string, data: any) => {
			return vm.httpClient.put<T>(baseUrl + secondayUrl, data).pipe(map((response: T) => {
				return response;
			}));
		};

		vm.delete = <T>(baseUrl: string, secondayUrl: string) => {
			return vm.httpClient.delete<T>(baseUrl + secondayUrl).pipe(map((response: T) => {
				return response;
			}));
		};

	}

}
