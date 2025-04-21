export interface ResponseHistorialBibliotecaDto {
  prestamoId: number;
  cantidadPrestada: number;
  fechaPrestamo: string; 
  estado: boolean;
  fechaDevolucion?: string; 

  nombreUsuario: string;
  cedulaUsuario: string;
  rolUsuario: string;

  tituloMaterial: string;
  nroIdentificacionMaterial: string;
  tipoMaterial: string;
}
