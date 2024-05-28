import { Component, Inject, OnInit,inject } from '@angular/core';
import { MatDialog, MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Sucursales } from '../../interfaces/sucursales';
@Component({
  selector: 'app-dialog-delete',
  templateUrl: './dialog-delete.component.html',
  styleUrl: './dialog-delete.component.css'
})
export class DialogDeleteComponent implements OnInit
{
  tituloAccion:string = 'Eliminar';
  botonAccion:string = "Eliminar";
  errorMessage: string | null = null;

  constructor(
    private dialogoReferencia:MatDialogRef<DialogDeleteComponent>,
    //recibimos la data del app-component que nos envian por el dialog
    @Inject(MAT_DIALOG_DATA) public eliminar:Sucursales
  )
  {

  }


  ngOnInit(): void
  {
    //throw new Error('Method not implemented.');
  }

  confirmarEliminar()
  {
    if(this.eliminar)
    {
      this.dialogoReferencia.close("eliminar");
    }
  }
}
