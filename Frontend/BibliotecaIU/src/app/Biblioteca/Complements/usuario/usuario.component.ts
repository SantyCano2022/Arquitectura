import { Component, OnInit } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { TooltipModule } from 'primeng/tooltip';
import { InputTextModule } from 'primeng/inputtext';
import { MessageService } from 'primeng/api';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { usuarioService } from '../../Services/usuario.service';
import { Router } from '@angular/router';
import { ConsultarUsuarioDto } from '../../Models/ConsultarUsuarioDto';
import { ObtenerRolesDto } from '../../Models/ObtenerRolesDto';
import { rolService } from '../../Services/rol.service';
import { DropdownModule } from 'primeng/dropdown';

@Component({
  selector: 'app-usuario',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TableModule,
    ButtonModule,
    ToastModule,
    TooltipModule,
    InputTextModule,
    HttpClientModule,
    DialogModule,
    DropdownModule 
  ],
  providers: [MessageService, usuarioService, rolService],
  templateUrl: './usuario.component.html',
  styleUrl: './usuario.component.css'
})
export class UsuarioComponent implements OnInit {

  formularioUsuario!: FormGroup;
  titulo: string = "Administrar Usuarios";
  rowsPerPage = 5;
  usuarios: ConsultarUsuarioDto[] = [];
  roles: ObtenerRolesDto[] = [];
  mostrarCrearDialogo: boolean = false;

  constructor(
    private messageService: MessageService,
    private _usuarioService: usuarioService,
    private _rolService: rolService,
    private fb: FormBuilder,
    private router: Router
  ) { 
  }

  ngOnInit(): void {
    this.ObtenerUsuarios();
    this.obtenerRoles();

    this.formularioUsuario = this.fb.group({
      nombre: ['', Validators.required],
      cedula: ['', Validators.required],
      rolId: [null, Validators.required],
    });
  }

  ObtenerUsuarios(){
    this._usuarioService.ObtenerUsuarios().subscribe({
      next: (data) => {
        this.usuarios = data;
      }, 
      error: (err) => {
        this.messageService.add({
          severity: 'error',
          summary: 'No hay registros de usuarios',
          detail: '',
        });
      }
    });
  }

  eliminarUsuario(id: number): void {
    this._usuarioService.eliminarUsuario(id).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Éxito', detail: 'Usuario eliminado correctamente' });
        this.ObtenerUsuarios(); // Actualiza la lista de usuarios
      },
      error: () => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'No se pudo eliminar el usuario' });
      }
    });
}
  mostrarDialogo() {
    this.mostrarCrearDialogo = true;
  }
  
  cerrarDialogo() {
    this.formularioUsuario.reset();
    this.mostrarCrearDialogo = false;
  }
  
  obtenerRoles() {
    this._rolService.ObtenerUsuarios().subscribe({
      next: (data) => this.roles = data,
      error: () => this.messageService.add({
        severity: 'error',
        summary: 'Error al obtener roles',
        detail: '',
      })
    });
  }
  
  guardarUsuario() {
    if (this.formularioUsuario.valid) {
      this._usuarioService.CrearUsuario(this.formularioUsuario.value).subscribe({
        next: (response: any) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Éxito',
            detail: response.message
          });
          this.formularioUsuario.reset();
          this.mostrarCrearDialogo = false;
          this.ObtenerUsuarios();
        },
        error: (err) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: err.error?.message || 'Error al registrar el usuario.'
          });
        }
      });
    }
  }
  
  redireccionarHistorial() {
    this.router.navigate(['/']);
  }

}
