import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { environment } from "../../../environments/environment";
import { ConsultarUsuarioDto } from "../Models/ConsultarUsuarioDto";

@Injectable({
    providedIn: 'root'
})
export class usuarioService {
    UsuarioAPI : string;

    private _refresh = new Subject<void>();

    get refresh() {
        return this._refresh;
    }

    constructor(private http: HttpClient) {
        this.UsuarioAPI = `${environment.apiUrl}/Usuario/`;
        
    }

    ObtenerUsuarios(): Observable<ConsultarUsuarioDto[]> {
        return this.http.get<ConsultarUsuarioDto[]>(`${this.UsuarioAPI}ObtenerUsuarios`);
    }

    eliminarUsuario(id: number): Observable<any> {
        return this.http.delete(`${this.UsuarioAPI}EliminarUsuario/${id}`);
    }
    
    CrearUsuario(Usuario : any): Observable<any> {
        return this.http.post(`${this.UsuarioAPI}RegistrarUsuario`, Usuario)
    }

}