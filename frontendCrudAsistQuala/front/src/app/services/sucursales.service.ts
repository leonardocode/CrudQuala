import { Injectable } from '@angular/core';
//importamos las interfaces
import { Sucursales } from './../interfaces/sucursales';
//importamos el httpclient
import { HttpClient } from '@angular/common/http';
//importamos los environment que contiene la url de los servicios
import { environment } from '../../environments/environment';
//importamos los observables
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SucursalesService
{
  private endPoint:string = environment.endPoint;
  private apiUrl:string = this.endPoint + "api/Sucursal/";
  constructor(private http:HttpClient) { }

  ConsultarSucursales():Observable<Sucursales[]>
  {
    return this.http.get<Sucursales[]>(`${this.apiUrl}ConsultarSucursales`);
  }

  AgregarSucursal(sucursal:Sucursales):Observable<Sucursales>
  {
    //se consume la api y se le envia los datos de la sucursal a agregar
    return this.http.post<Sucursales>(`${this.apiUrl}Guardar`,sucursal);
  }

  ActualizarSucursal(idSucursal:number,sucursal:Sucursales):Observable<Sucursales>
  {
    //se consume la api y se le envia los datos de la sucursal a agregar
    return this.http.put<Sucursales>(`${this.apiUrl}Editar/${idSucursal}`,sucursal);
  }

  EliminarSucursal(idSucursal:number):Observable<void>
  {
    //se consume la api y se le envia los datos de la sucursal a agregar
    return this.http.delete<void>(`${this.apiUrl}Eliminar/${idSucursal}`);
  }
}
