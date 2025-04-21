import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { environment } from "../../../environments/environment";
import { ObtenerMaterialesDto } from "../Models/ObtenerMaterialesDto";
import { CrearMaterialDto } from "../Models/CrearMaterialDto";

@Injectable({
    providedIn: 'root'
})
export class materialService {
    MaterialAPI : string;

    private _refresh = new Subject<void>();

    get refresh() {
        return this._refresh;
    }

    constructor(private http: HttpClient) {
        this.MaterialAPI = `${environment.apiUrl}/Material/`;
    }

    ObtenerMateriales(): Observable<ObtenerMaterialesDto[]> {
        return this.http.get<ObtenerMaterialesDto[]>(`${this.MaterialAPI}ObtenerMateriales`);
    }
    
    CrearMaterial(material : CrearMaterialDto): Observable<any> {
        return this.http.post(`${this.MaterialAPI}RegistrarMaterial`, material)
    }

    ActualizarMaterial(materialAActualizar: { id: number, cantidad: number }) {
        return this.http.put(`${this.MaterialAPI}ActualizarMaterial`, materialAActualizar)
    }
}