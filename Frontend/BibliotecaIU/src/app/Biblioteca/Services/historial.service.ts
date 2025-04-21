import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { environment } from "../../../environments/environment";
import { ResponseHistorialBibliotecaDto } from "../Models/ResponseHistorialBibliotecaDto";
import { CrearPrestamoDto } from "../Models/CrearPrestamoDto";

@Injectable({
    providedIn: 'root'
})
export class historialService {
    PrestamoAPI : string;
    DevolucionAPI : string;

    private _refresh = new Subject<void>();

    get refresh() {
        return this._refresh;
    }

    constructor(private http: HttpClient) {
        this.PrestamoAPI = `${environment.apiUrl}/Prestamo/`;
        this.DevolucionAPI = `${environment.apiUrl}/Devolucion/`;
        
    }

    historialBiblioteca(): Observable<ResponseHistorialBibliotecaDto[]> {
        return this.http.get<ResponseHistorialBibliotecaDto[]>(`${this.PrestamoAPI}ObtenerHistorial`);
    }
    
    devolucionPrestamo(idPrestamo: number): Observable<any> {
        return this.http.post<any>(`${this.DevolucionAPI}RegistrarDevolucion/${idPrestamo}`, null);
    }

    solicitarPrestamo(prestamo: any): Observable<any> {
        return this.http.post<any>(`${this.PrestamoAPI}RegistrarPrestamo`, prestamo);
    }
}