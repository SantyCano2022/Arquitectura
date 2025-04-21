import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { environment } from "../../../environments/environment";
import { ConsultarUsuarioDto } from "../Models/ConsultarUsuarioDto";
import { TipoMaterial } from "../Models/TipoMaterial";

@Injectable({
    providedIn: 'root'
})
export class tipoMaterialService {
    TipoMaterialAPI : string;

    private _refresh = new Subject<void>();

    get refresh() {
        return this._refresh;
    }

    constructor(private http: HttpClient) {
        this.TipoMaterialAPI = `${environment.apiUrl}/TipoMaterial/`;
    }

    ObtenerTiposMaterial(): Observable<TipoMaterial[]> {
        return this.http.get<TipoMaterial[]>(`${this.TipoMaterialAPI}ObtenerTipoMaterial`);
    }

}