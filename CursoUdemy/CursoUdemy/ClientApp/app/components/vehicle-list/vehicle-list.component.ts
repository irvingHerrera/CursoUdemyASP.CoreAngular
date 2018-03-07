import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../../services/vehicle.service';
import { Vehicle, KeyValuePair } from '../../models/vehicle';

@Component({
    selector: 'app-vehicle-list',
    templateUrl: './vehicle-list.component.html',
    styleUrls: ['./vehicle-list.component.css']
})

export class VehicleListComponent implements OnInit {
    vehicles: Vehicle[];
    makes: KeyValuePair[];
    query: any = {
        pageSize: 3
    };
    columns = [
        { title: 'id' },
        { title: 'Contact Name', key: 'contactName', isSortable: true },
        { title: 'Make', key: 'make', isSortable: true },
        { title: 'Model', key: 'model', isSortable: true }
    ];

    constructor(private vehicleService: VehicleService) { }

    ngOnInit() {

        this.vehicleService.getMakes()
            .subscribe(makes => { this.makes = makes; })

        this.populateVehicles();
    }

    private populateVehicles() {
        this.vehicleService.getVehicles(this.query)
            .subscribe(vehicles => this.vehicles = vehicles);
    }

    onFilterChange() {
        //filtro local
        //var vehicles = this.allVehicles;

        //if (this.query.makeId)
        //    vehicles = vehicles.query(v => v.make.id == this.query.makeId);

        //if (this.query.modelId)
        //    vehicles = vehicles.query(v => v.model.id == this.query.modelId);

        //this.vehicles = vehicles;

        this.populateVehicles();
    }

    resetFilter() {
        this.query = {};
        this.onFilterChange();
    }

    sortBy(columnName: string) {
        if (this.query.sortBy === columnName) {
            this.query.isSortAscending = !this.query.isSortAscending;
        } else {
            this.query.sortBy = columnName;
            this.query.isSortAscending = true;
        }

        this.populateVehicles();
    }

    onPageChange(page: any) {
        this.query.page = page;
        this.populateVehicles();
    }

}
