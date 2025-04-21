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
import { Router } from '@angular/router';
import { DropdownModule } from 'primeng/dropdown';
import { materialService } from '../../Services/material.service';
import { ObtenerMaterialesDto } from '../../Models/ObtenerMaterialesDto';
import { CrearMaterialDto } from '../../Models/CrearMaterialDto';
import { TipoMaterial } from '../../Models/TipoMaterial';
import { tipoMaterialService } from '../../Services/tipoMaterial.service';

@Component({
  selector: 'app-material',
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
  providers: [MessageService, materialService, tipoMaterialService],
  templateUrl: './material.component.html',
  styleUrl: './material.component.css'
})
export class MaterialComponent implements OnInit {

  titulo: string = "Administrar Materiales";
  rowsPerPage = 5;
  materiales: ObtenerMaterialesDto[] = [];
  tiposMaterial: TipoMaterial[] = [];

  mostrarDialogo: boolean = false;
  formularioMaterial!: FormGroup;
  mostrarDialogoSumar: boolean = false;
  formularioSuma!: FormGroup;
  materialSeleccionado!: ObtenerMaterialesDto;

  constructor(
    private router: Router,
    private _materialService: materialService,
    private _tipoMaterialService: tipoMaterialService,
    private fb: FormBuilder,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.ObtenerMateriales();
    this.ObtenerTiposMaterial();
    this.inicializarFormulario();
    this.inicializarFormularioSuma();
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

  ObtenerTiposMaterial() {
    this._tipoMaterialService.ObtenerTiposMaterial().subscribe({
      next: (data) => this.tiposMaterial = data,
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'No se pudieron cargar los tipos de material',
        });
      }
    });
  }

  inicializarFormulario() {
    this.formularioMaterial = this.fb.group({
      titulo: ['', Validators.required],
      nroIdentificacion: ['', Validators.required],
      cantidadRegistrada: [0, [Validators.required, Validators.min(1)]],
      cantidadActual: [0, [Validators.required, Validators.min(0)]],
      tipoMaterialId: [null, Validators.required]
    });
  }

  guardarMaterial() {
    if (this.formularioMaterial.invalid) return;

    const nuevoMaterial: CrearMaterialDto = this.formularioMaterial.value;

    this._materialService.CrearMaterial(nuevoMaterial).subscribe({
      next: () => {
        this.messageService.add({
          severity: 'success',
          summary: 'Éxito',
          detail: 'Material registrado correctamente'
        });
        this.mostrarDialogo = false;
        this.ObtenerMateriales();
        this.formularioMaterial.reset();
      },
      error: (err) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: err.error?.message
        });
      }
    });
  }

  inicializarFormularioSuma() {
    this.formularioSuma = this.fb.group({
      cantidad: [0, [Validators.required, Validators.min(1)]]
    });
  }

  abrirDialogoSumar(material: ObtenerMaterialesDto) {
    this.materialSeleccionado = material;
    this.formularioSuma.reset();
    this.mostrarDialogoSumar = true;
  }

  sumarCantidad() {
    if (this.formularioSuma.invalid || !this.materialSeleccionado) return;
  
    const cantidadSuma = this.formularioSuma.value.cantidad;
    const idMaterial = this.materialSeleccionado.id;
  
    const materialAActualizar = {
      id: idMaterial,
      cantidad: cantidadSuma
    };
  
    this._materialService.ActualizarMaterial(materialAActualizar).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Actualizado', detail: 'Material actualizado correctamente' });
        this.mostrarDialogoSumar = false;
        this.ObtenerMateriales(); 
      },
      error: (err) => {
        console.error('Error en la actualización:', err);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: err.error?.message });
      }
    });
  }
  
  

  redireccionarHistorial() {
    this.router.navigate(['/']);
  }
}