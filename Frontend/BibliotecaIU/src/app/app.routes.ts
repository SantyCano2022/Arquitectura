import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./Biblioteca/Complements/historial/historial.component').then(m => m.HistorialComponent),
  },
  {
    path: 'Material',
    loadComponent: () =>
      import('./Biblioteca/Complements/material/material.component').then(m => m.MaterialComponent),
  },
  {
    path: 'Usuario',
    loadComponent: () =>
      import('./Biblioteca/Complements/usuario/usuario.component').then(m => m.UsuarioComponent),
  },
];
