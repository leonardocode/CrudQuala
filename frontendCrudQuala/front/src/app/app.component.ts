import {AfterViewInit, Component, ViewChild, OnInit} from '@angular/core';
import {
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogTitle,
} from '@angular/material/dialog';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { Sucursales } from './interfaces/sucursales';
import { SucursalesService } from './services/sucursales.service';
import { AgregarEditarComponent } from './components/agregar-editar/agregar-editar.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DialogDeleteComponent } from './components/dialog-delete/dialog-delete.component';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements AfterViewInit,OnInit
{

  //filtro
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  //origen de columnas
  displayedColumns: string[] = ['Codigo', 'Descripcion', 'Direccion', 'fechaCreacion', 'Moneda','Editar','Eliminar'];

  //origen de datos
  dataSource = new MatTableDataSource<Sucursales>();

  //importa el paginador
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    //llamamos en un modal el componente del formulario
    this.dataSource.paginator = this.paginator;
  }

  //llamamos el modal para hacer el formulario de insercion de datos
  NuevaSucursal()
  {
    this.dialog.open(AgregarEditarComponent, {
      //si se presiona por fuera del modal no se cierre
      disableClose:true,
      //le agrandamso el ancho al dialog
      width:"350px"
    }).afterClosed().subscribe(resultado => {
      if(resultado === 'creado')
      {
        //si fue creada la sucursal correctamente llamamos el listado de las sucursales
        this.obtenerSucursales()
      }
    });
  }

 //por inyeccion de dependencias
  constructor(
    private sucursalService:SucursalesService,
    public dialog: MatDialog,
    public snackBar:MatSnackBar)
  {

  }


    //mostramos toastr
    mostrarAlerta(msg: string, accion: string)
    {
      this.snackBar.open(msg, accion, {
        horizontalPosition:"end",
        verticalPosition:"top",
        duration:3000
      });
    }


  ngOnInit(): void
   {
    //ejecuta una logica cuando el componente inicie
    //throw new Error('Method not implemented.');
    this.obtenerSucursales();
   }

   obtenerSucursales()
   {
    this.sucursalService.ConsultarSucursales().subscribe({
      next:(dataResponse)=>
      {
        console.log(dataResponse)
        //llenamos la informacion del dataSource d ela tabla con la respuesta del get del servicio
        this.dataSource.data = dataResponse;
      }, error:(e) =>{
        console.log(e);
      }
    })
   }

   EditarSucursal(editar:Sucursales)
   {
      this.dialog.open(AgregarEditarComponent, {
        //si se presiona por fuera del modal no se cierre
        disableClose:true,
        //le agrandamso el ancho al dialog
        width:"350px",
        //le enviamos al modal la informacion que queremos editar
        data:editar
      }).afterClosed().subscribe(resultado => {
        if(resultado === 'editado')
        {
          //si fue creada la sucursal correctamente llamamos el listado de las sucursales
          this.obtenerSucursales()
        }
      });

      //obtenemos la informacion por el id que queremos editrar
    //console.log(idSucursal);
    // this.sucursalService.ConsultarSucursalPorId(idSucursal).subscribe({
    //   next:(dataResponse)=>
    //     {
    //       console.log('consultarPorId',dataResponse)
    //       //llenamos la informacion del dataSource d ela tabla con la respuesta del get del servicio
    //       this.dataSource.data = dataResponse;
    //     }, error:(e) =>{
    //       console.log(e);
    //     }
    // })
   }


   EliminarSucursal(eliminar:Sucursales)
   {
      this.dialog.open(DialogDeleteComponent, {
        //si se presiona por fuera del modal no se cierre
        disableClose:true,
        //le agrandamso el ancho al dialog
        width:"350px",
        //le enviamos al modal la informacion que queremos editar
        data:eliminar
      }).afterClosed().subscribe(resultado => {
        if(resultado === 'eliminar')
        {
          console.log('codigo a eliminar: '+eliminar.codigo);
          //si fue creada la sucursal correctamente llamamos el listado de las sucursales
          this.sucursalService.EliminarSucursal(eliminar.codigo).subscribe({
            next:(data)=>
              {
                this.mostrarAlerta("Empleado fue eliminado","Listo");
                this.obtenerSucursales();
              },
              error:(e) =>
                {
                  this.mostrarAlerta("Error: "+e.error,"Error");
                }
          })
        }
      });
   }


}


