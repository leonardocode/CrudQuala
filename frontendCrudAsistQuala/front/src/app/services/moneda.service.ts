import { Injectable } from '@angular/core';
//importamos el httpclient
import { HttpClient } from '@angular/common/http';
//importamos los environment que contiene la url de los servicios
import { environment } from '../../environments/environment';
//importamos los observables
import { Observable } from 'rxjs';
//importamos las interfaces
import { Moneda } from '../interfaces/moneda';

@Injectable({
  providedIn: 'root'
})
export class MonedaService
{
  private endPoint:string = environment.endPoint;
  private apiUrl:string = this.endPoint + "api/Moneda/";

  constructor(private http:HttpClient) { }

  ConsultarMonedas():Observable<Moneda[]>
  {
    return this.http.get<Moneda[]>(`${this.apiUrl}ConsultarMonedas`);
  }
}
