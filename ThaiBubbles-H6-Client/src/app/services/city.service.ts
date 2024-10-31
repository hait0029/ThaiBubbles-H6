import { Injectable } from '@angular/core';
import { environment } from '../../environments/environments';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { City } from '../Models/City';

@Injectable({
  providedIn: 'root',
})
export class CityService {
  private apiUrl = environment.apiurl + 'City';
  constructor(private http: HttpClient) {}

  getAll(): Observable<City[]> {
    return this.http.get<City[]>(this.apiUrl);
  }
  getById(cityId: number): Observable<City> {
    return this.http.get<City>(`${this.apiUrl}/${cityId}`);
  }
  create(city: City): Observable<City> {
    return this.http.post<City>(this.apiUrl, city);
  }

  update(city: City): Observable<City> {
    return this.http.put<City>(`${this.apiUrl}/${city.cityID}`, city);
  }

  delete(cityId: number): Observable<City> {
    return this.http.delete<City>(`${this.apiUrl}/${cityId}`);
  }
}
