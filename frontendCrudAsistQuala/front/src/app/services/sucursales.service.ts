import { Injectable } from '@angular/core';
//importamos las interfaces
import { Sucursales } from './../interfaces/sucursales';
//importamos el httpclient
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
//importamos los environment que contiene la url de los servicios
import { environment } from '../../environments/environment';
//importamos los observables
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SucursalesService
{
  private endPoint:string = environment.endPoint;
  private apiUrl:string = this.endPoint + "api/Sucursal/";
  constructor(private http:HttpClient,private _snackBar:MatSnackBar,) { }

  mostrarAlerta(msg: string, accion: string)
  {
    this._snackBar.open(msg, accion, {
      horizontalPosition:"end",
      verticalPosition:"top",
      duration:5000
    });
  }

  ConsultarSucursales():Observable<Sucursales[]>
  {
    console.log('url:',this.http.get<Sucursales[]>(`${this.apiUrl}ConsultarSucursales`));
    return this.http.get<Sucursales[]>(`${this.apiUrl}ConsultarSucursales`);
  }

  ConsultarSucursalPorId(id:number):Observable<Sucursales[]>
  {
    console.log('url:',this.http.get<Sucursales[]>(`${this.apiUrl}obtenerSucursalPorCodigo?Codigo=${id}`));
    return this.http.get<Sucursales[]>(`${this.apiUrl}obtenerSucursalPorCodigo?Codigo=${id}`);
  }


  AgregarSucursal(sucursal:Sucursales):Observable<Sucursales>
  {
    //se consume la api y se le envia los datos de la sucursal a agregar
    return this.http.post<Sucursales>(`${this.apiUrl}Guardar`,sucursal);
  }

  ActualizarSucursal(sucursal:Sucursales):Observable<Sucursales>
  {
    console.log()
    //se consume la api y se le envia los datos de la sucursal a agregar
    return this.http.put<Sucursales>(`${this.apiUrl}Editar`,sucursal);
  }

  EliminarSucursal(idSucursal:number):Observable<void>
  {
    //se consume la api y se le envia los datos de la sucursal a agregar
    return this.http.delete<void>(`${this.apiUrl}Eliminar?Codigo=${idSucursal}`);
  }


}
