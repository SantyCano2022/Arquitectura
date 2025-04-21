import { Component, OnInit } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { TooltipModule } from 'primeng/tooltip';
import { InputTextModule } from 'primeng/inputtext';
import { MessageService } from 'primeng/api';
import { historialService } from '../../Services/historial.service';
import { ResponseHistorialBibliotecaDto } from '../../Models/ResponseHistorialBibliotecaDto';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { CrearPrestamoDto } from '../../Models/CrearPrestamoDto';
import { ObtenerMaterialesDto } from '../../Models/ObtenerMaterialesDto';
import { DropdownModule } from 'primeng/dropdown';
import { materialService } from '../../Services/material.service';
import { usuarioService } from '../../Services/usuario.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ConsultarUsuarioDto } from '../../Models/ConsultarUsuarioDto';

@Component({
  selector: 'app-historial',
  standalone: true,
  imports: [ 
    CommonModule,
    TableModule,
    ButtonModule,
    ToastModule,
    TooltipModule,
    InputTextModule,
    HttpClientModule,
    DialogModule,
    DropdownModule,
    ReactiveFormsModule
  ],
  providers: [MessageService, historialService, materialService, usuarioService],
  templateUrl: './historial.component.html',
  styleUrl: './historial.component.css'
})
export class HistorialComponent implements OnInit {

  titulo: string = "Historial de Biblioteca"
  rowsPerPage = 5;
  prestamoForm!: FormGroup;
  historial: ResponseHistorialBibliotecaDto[] = [];
  usuarios: ConsultarUsuarioDto[] = []; 
  materiales: ObtenerMaterialesDto[] = [];
  selectedItem: ResponseHistorialBibliotecaDto | null = null;
  displayModal: boolean = false;
  displayPrestamoDialog: boolean = false; 
  
  constructor(
    private fb: FormBuilder,
    private messageService: MessageService,
    private _historialService: historialService,
    private _materialService: materialService,
    private _usuarioService: usuarioService,
    
    private router: Router
  ) { 
    
  }

  ngOnInit(): void {
    this.prestamoForm = this.fb.group({
      usuarioId: [null, Validators.required],
      materialId: [null, Validators.required],
      cantidadPrestada: [null, [Validators.required, Validators.min(1)]]
    });
    this.ObtenerHistorial();
  }

  ObtenerHistorial() {
    this._historialService.historialBiblioteca().subscribe({
      next: (value) => {
        this.historial = value;
      },
      error: (err) => {
        this.messageService.add({
          severity: 'error',
          summary: 'No hay registros de prestamos para un historial',
          detail: '',
        });
      }
    });
  }

  showDetails(item: ResponseHistorialBibliotecaDto) {
    this.selectedItem = item;
    this.displayModal = true;
  }

  registrarDevolucion(idPrestamo: number) {
    this._historialService.devolucionPrestamo(idPrestamo).subscribe({
      next: () => {
        this.messageService.add({
          severity: 'success',
          summary: 'Devolución registrada',
          detail: 'La devolución se ha registrado correctamente.'
        });
        this.ObtenerHistorial();
      },
      error: (err) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error al registrar devolución',
          detail: err.error.message || 'Ocurrió un error inesperado.'
        });
      }
    });
  }

  showSolicitarPrestamoDialog() {
    this.displayPrestamoDialog = true;
    this.ObtenerMateriales();
    this.ObtenerUsuarios();
  }

  closePrestamoDialog() {
    this.displayPrestamoDialog = false;
  }

  ObtenerMateriales() {
    this._materialService.ObtenerMateriales().subscribe({
      next: (data) => this.materiales = data,
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'No se pudieron cargar los materiales',
        });
      }
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

  solicitarPrestamo() {
    if (this.prestamoForm.valid) {
      const prestamoData: CrearPrestamoDto = this.prestamoForm.value;
      this._historialService.solicitarPrestamo(prestamoData).subscribe({
        next: (response) => { 
          this.messageService.add({
            severity: 'success',
            summary: 'Préstamo solicitado',
            detail: response.message
          });
          this.closePrestamoDialog();
          this.ObtenerHistorial();
        },
        error: (err) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error al solicitar préstamo',
            detail: err.error.message || 'Ocurrió un error inesperado.'
          });
        }
      });
    }
 }
 
  redirectToAgregarUsuario() {
    this.router.navigate(['/Usuario']);
  }

  redirectToAgregarMaterial() {
    this.router.navigate(['/Material']); 
  }
}
