
import { Column } from 'primereact/column';
import { DataTable } from 'primereact/datatable';
import { Button } from 'primereact/button';
import React, { useEffect, useState } from 'react';
import EmployeeService from './EmployeeService';
import moment from 'moment';
import './App.css';
import 'primereact/resources/primereact.min.css'; // Import PrimeReact CSS
import 'primereact/resources/themes/bootstrap4-dark-blue/theme.css'; // Import PrimeReact theme CSS


export default function PaginatorBasicDemo() {
    const [employees, setEmployees] = useState([]);

    const formatDate = (rowData) => {
        const dateJoined = moment(rowData.dateJoined).format('YYYY-MMM-DD'); // Format the date as desired
        return <span>{dateJoined}</span>;
    };

    const employeeService = new EmployeeService();

    const deleteEmployee = async (id) => {
        try {
            await employeeService.deleteEmployee(id);
            // Update employees state after deletion
           // setEmployees(employees.filter(emp => emp.id !== id));
            employeeService.getEmployees().then((data) => {
                setEmployees(data)
            });
        } catch (error) {
            console.error('Error deleting employee:', error);
            // Handle error if necessary
        }
    };

    const actionTemplate = (rowData) => {
        return (
            <Button icon="pi pi-trash" className="p-button-danger" onClick={() => deleteEmployee(rowData.employeeId)} />
        );
    };

    useEffect(() => {
        employeeService.getEmployees().then((data) => {
            setEmployees(data)
        });
    }, []);

    return (
        <div className="table">
            <DataTable value={employees} sortMode="multiple" paginator rows={5} rowsPerPageOptions={[5, 10, 25, 50]} tableStyle={{ minWidth: '50rem' }} dataKey="employeeId" filterDisplay="row" globalFilterFields={['firstName', 'lastName', 'extension', 'RoleName']} emptyMessage="No employee found." paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink" showGridlines stripedRows>
                <Column field="employeeId" sortable header="EmpId" style={{ width: '13%' }}></Column>
                <Column field="employeeNumber" sortable header="Number" style={{ width: '13%' }}></Column>
                <Column field="firstName" filter filterPlaceholder="Search" sortable header="First Name" style={{ width: '16%' }}></Column>
                <Column field="lastName" filter filterPlaceholder="Search " sortable header="Last Name" style={{ width: '16%' }}></Column>
                <Column field="dateJoined" header="DoJ" body={formatDate} sortable style={{ width: '13%' }} />
                <Column field="extension" filter filterPlaceholder="Search " sortable header="Extension" style={{ width: '13%' }}></Column>
                <Column field="RoleName" filter filterPlaceholder="Search " sortable header="Role Name" style={{ width: '16%' }}></Column>
                <Column body={actionTemplate} style={{ textAlign: 'center', width: '6em' }} />
          </DataTable>
        </div>
    );
}
