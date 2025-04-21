import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { environment } from "../../../environments/environment";
import { ObtenerRolesDto } from "../Models/ObtenerRolesDto";

@Injectable({
    providedIn: 'root'
})
export class rolService {
    RolAPI : string;

    private _refresh = new Subject<void>();

    get refresh() {
        return this._refresh;
    }

    constructor(private http: HttpClient) {
        this.RolAPI = `${environment.apiUrl}/Rol/`;
        
    }

    ObtenerUsuarios(): Observable<ObtenerRolesDto[]> {
        return this.http.get<ObtenerRolesDto[]>(`${this.RolAPI}ObtenerRoles`);
    }

}