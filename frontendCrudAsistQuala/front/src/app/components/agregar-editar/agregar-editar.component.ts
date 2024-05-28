import { Component, Inject, OnInit,inject } from '@angular/core';
import { FormBuilder,FormGroup,Validators } from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatDialog, MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import moment from 'moment';
import { Sucursales } from '../../interfaces/sucursales';
import { Moneda } from '../../interfaces/moneda';
import { SucursalesService } from '../../services/sucursales.service';
import { MonedaService } from '../../services/moneda.service';
import { identifierName } from '@angular/compiler';
import { DatePipe } from '@angular/common';

//importamos los componentes para poder trabajar con la data enviada desde el app-component
//import { inject } from '@angular/core';

//para usar la informacion que es enviada a traves de los dialog
//import { MAT_DIALOG_DATA } from '@angular/material/dialog';


export const MY_DATE_FORMATS =
{
  parse:{
    dateInput:'DD/MM/YYYY'
  },
  display:{
    dateInput:'DD/MM/YYYY',
    montYearLabel:'MMMM YYYY',
    dateA11Label:'LL',
    montYearA11yLabel:'MMMM YYYY'
  }
}

@Component({
  selector: 'app-agregar-editar',
  templateUrl: './agregar-editar.component.html',
  styleUrl: './agregar-editar.component.css',
  providers:[
    {
      //usamos el formato de fecha que configuramos
      provide: MAT_DATE_FORMATS,useValue:MY_DATE_FORMATS
    }
  ]
})
export class AgregarEditarComponent implements OnInit
{
  //formulario
  formSucursal:FormGroup;
  tituloAccion:string = 'Nuevo';
  botonAccion:string = "Guardar";
  listaMonedas: Moneda[] = [];
  currentDate: Date;
  errorMessage: string | null = null;

  constructor(
    private dialogoReferencia:MatDialogRef<AgregarEditarComponent>,
    private fb: FormBuilder,
    private _snackBar:MatSnackBar,
    private _sucursalService:SucursalesService,
    private _monedaService:MonedaService,
    private datePipe: DatePipe,
    //recibimos la data del app-component que nos envian por el dialog
    @Inject(MAT_DIALOG_DATA) public editar:Sucursales
  )
  {
    this.currentDate = new Date();

    this.formSucursal = this.fb.group({
      codigo:[0, Validators.required],
      descripcion:['', Validators.required],
      direccion:['', Validators.required],
      identificacion:['', Validators.required],
      fechaCreacion:['',Validators.required],
      idMoneda:['', Validators.required]
    });

  }

  ngOnInit(): void {
    //throw new Error('Method not implemented.');
    this.mostrarMonedas();
    if (this.editar) {
      // Convertir la fecha recibida al formato ISO antes de usar moment
      const fechaCreacionISO = moment(this.editar.fechaCreacion, 'DD/MM/YYYY').toISOString();
      this.formSucursal.patchValue({
        codigo: this.editar.codigo,
        descripcion: this.editar.descripcion,
        direccion: this.editar.direccion,
        identificacion: this.editar.identificacion,
        fechaCreacion: moment(fechaCreacionISO).toDate(),
        idMoneda: this.editar.idMoneda
      });
      this.tituloAccion = 'Editar';
      this.botonAccion = 'Actualizar';
    }
  }


  mostrarMonedas()
  {
     //obtenemos la informacion de monedas
     this._monedaService.ConsultarMonedas().subscribe
     ({
       next:(data)=>
         {
           console.log('Monedas:',data);
           this.listaMonedas = data;
         },
         error:(e) =>
         {
             console.log('error monedas:',e);
         }
     })
  }


  //mostramos toastr
  mostrarAlerta(msg: string, accion: string)
  {
    this._snackBar.open(msg, accion, {
      horizontalPosition:"end",
      verticalPosition:"top",
      duration:3000
    });
  }

  AgregarEditarSucursal()
  {
    if (this.formSucursal.invalid) {
      return
    }
    console.log(this.formSucursal);
    console.log(this.formSucursal.value);

    const sucursal:Sucursales =
    {
      codigo:0,
      descripcion:this.formSucursal.value.descripcion,
      direccion:this.formSucursal.value.direccion,
      identificacion:this.formSucursal.value.identificacion,
      fechaCreacion: moment(this.formSucursal.value.fechaCreacion).format('DD/MM/YYYY'),
      idMoneda:this.formSucursal.value.idMoneda,
      moneda:''
    }

    //si no estamos editando
    if(this.editar == null)
    {
      this._sucursalService.AgregarSucursal(sucursal).subscribe({
        next:(data) =>
          {
            //console.log('insertar informacion:',data);
            this.mostrarAlerta('Sucursal Registrada correctamente','Listo');
            this.dialogoReferencia.close("creado");
            this.errorMessage = null;
          },error:(e) => {
            console.log('informacion enviada', sucursal);
            this.mostrarAlerta('Error: ' + e.error,'Error');
            this.errorMessage = e;
          }
      })
    }
    else
    {
      console.log('codigo editar',this.formSucursal.value.fechaCreacion);
      console.log('codigo ',this.editar.fechaCreacion);
      sucursal.codigo = this.editar.codigo;
      sucursal.fechaCreacion = moment(this.formSucursal.value.fechaCreacion).format('DD/MM/YYYY');
      this._sucursalService.ActualizarSucursal(sucursal).subscribe({
        next:(data) =>
          {
            //console.log('insertar informacion:',data);
            this.mostrarAlerta('Sucursal Actualizada correctamente','Listo');
            this.dialogoReferencia.close("editado");
            this.errorMessage = null;
          },error:(e) => {
            console.log('informacion enviada', sucursal);
            this.mostrarAlerta('Error: ' + e.error,'Error');
            this.errorMessage = e;
          }
      })
    }



  }

}
