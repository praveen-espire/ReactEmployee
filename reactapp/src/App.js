import { Column } from 'primereact/column';
import { DataTable } from 'primereact/datatable';
import { Button } from 'primereact/button';
import React, { useEffect, useState, useRef } from 'react';
import EmployeeService from './EmployeeService';
import moment from 'moment';
import './App.css';
import 'primereact/resources/primereact.min.css';
import 'primereact/resources/themes/bootstrap4-dark-blue/theme.css';
import { ConfirmDialog } from 'primereact/confirmdialog';
import { Dialog } from 'primereact/dialog';
import { Dropdown } from 'primereact/dropdown';

export default function EmployeeStatus() {
    const [employees, setEmployees] = useState([]);
    const [employeeIdToDelete, setEmployeeIdToDelete] = useState(null);
    const [confirmationVisible, setConfirmationVisible] = useState(false);
    const [displayDialog, setDisplayDialog] = useState(false);
    const [newEmployee, setNewEmployee] = useState({});
    const employeeService = new EmployeeService();
    const confirmDeleteRef = useRef(null);

    const formatDate = (rowData) => {
        const dateJoined = moment(rowData.dateJoined).format('YYYY-MMM-DD');
        return <span>{dateJoined}</span>;
    };

    const deleteEmployee = async (id) => {
        try {
            await employeeService.deleteEmployee(id);
            setEmployees(employees.filter(emp => emp.employeeId !== id));
            setConfirmationVisible(false);
        } catch (error) {
            console.error('Error deleting employee:', error);
        }
    };

    const addEmployee = async () => {
        try {
            await employeeService.addEmployee(newEmployee);
            //setEmployees([...employees, newEmployee]);
            employeeService.getEmployees().then((data) => {
                setEmployees(data)
            });
            setNewEmployee({});
            setDisplayDialog(false);
        } catch (error) {
            console.error('Error adding employee:', error);
        }
    };

    const confirmDelete = (id) => {
        setEmployeeIdToDelete(id);
        setConfirmationVisible(true);
    };

    const roleOptions = [
        { name: 'Junior Developer', value: '1' },
        { name: 'Senior Developer', value: '2' },
        { name: 'Project Manager', value: '3' },
        { name: 'Senior Architect', value: '4' },
        { name: 'Product Manager', value: '5' },
        { name: 'Intern', value: '6' },
        { name: 'Managing Director', value: '7' },
        { name: 'UX Designer', value: '8' },
        { name: 'Receptionist', value: '9' },
        { name: 'QA Team Lead', value: '10' },
        { name: 'Scrummaster', value: '11' },
    ];

    const actionTemplate = (rowData) => {
        return (
            <Button label="Delete" className="p-button-danger" onClick={() => confirmDelete(rowData.employeeId)} />
        );
    };

    useEffect(() => {
        employeeService.getEmployees().then((data) => {
            setEmployees(data)
        });
    }, []);


    return (
        <div className="table">
            <div className="container">
                <div className="row">
                    <div className="col">
                        {/* Content */}
                    </div>
                    <div className="col-auto">
                        <Button label="Add New Employee" onClick={() => setDisplayDialog(true)} className="p-button-primary" />
                    </div>
                </div>
            </div>
            <DataTable value={employees} sortMode="multiple" paginator rows={5} rowsPerPageOptions={[5, 10, 25, 50]} tableStyle={{ minWidth: '50rem' }} dataKey="employeeId" filterDisplay="row" globalFilterFields={['firstName', 'lastName', 'extension', 'RoleName']} emptyMessage="No employee found." paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink" showGridlines stripedRows>
                <Column field="employeeId" sortable header="EmpId" style={{ width: '13%' }}></Column>
                <Column field="employeeNumber" filter filterPlaceholder="Search" sortable header="Number" style={{ width: '13%' }}></Column>
                <Column field="firstName" filter filterPlaceholder="Start With" sortable header="First Name" style={{ width: '16%' }}></Column>
                <Column field="lastName" filter filterPlaceholder="Start With " sortable header="Last Name" style={{ width: '16%' }}></Column>
                <Column field="dateJoined" header="DoJ" body={formatDate} sortable style={{ width: '13%' }} />
                <Column field="extension" filter filterPlaceholder="Search" sortable header="Extension" style={{ width: '13%' }}></Column>
                <Column field="RoleName" filter filterPlaceholder="Search " sortable header="Role Name" style={{ width: '16%' }}></Column>
                <Column body={actionTemplate} style={{ textAlign: 'center', width: '6em' }} />
            </DataTable>
            <ConfirmDialog visible={confirmationVisible} onHide={() => setConfirmationVisible(false)} ref={confirmDeleteRef}
                message="Are you sure you want to delete this employee?" header="Confirmation" icon="pi pi-exclamation-triangle"
                acceptClassName="p-button-danger" rejectClassName="p-button-secondary" acceptLabel="Yes" rejectLabel="No"
                accept={() => deleteEmployee(employeeIdToDelete)} />
            <Dialog visible={displayDialog} onHide={() => setDisplayDialog(false)} header="Add New Employee" modal style={{ width: '25vw' }}>
                <div className="p-fluid">
                    <div className="p-field" style={{ display: 'none' }}>
                        <div className="row"> <label htmlFor="employeeId">Employee Id</label></div>
                        <input id="employeeId" type="text" value={newEmployee.employeeId || ''} onChange={(e) => setNewEmployee({ ...newEmployee, employeeId: 0 })} />
                    </div>
                    <div className="p-field">
                        <div className="row"> <label htmlFor="employeeNumber">Employee Number</label></div>
                        <input id="employeeNumber" type="text" value={newEmployee.employeeNumber || ''} onChange={(e) => setNewEmployee({ ...newEmployee, employeeNumber: e.target.value })} />
                    </div>
                    <div className="p-field">
                        <div className="row"> <label htmlFor="firstName">First Name</label></div>
                        <input id="firstName" type="text" value={newEmployee.firstName || ''} onChange={(e) => setNewEmployee({ ...newEmployee, firstName: e.target.value })} />
                    </div>
                    <div className="p-field">
                        <div className="row"> <label htmlFor="lastName">Last Name</label></div>
                        <input id="lastName" type="text" value={newEmployee.lastName || ''} onChange={(e) => setNewEmployee({ ...newEmployee, lastName: e.target.value })} />
                    </div>
                    <div className="p-field">
                        <div className="row"> <label htmlFor="dateJoined">Date Joined</label></div>
                        <input id="dateJoined" type="date" value={newEmployee.dateJoined || ''} onChange={(e) => setNewEmployee({ ...newEmployee, dateJoined: e.target.value })} />
                    </div>
                    <div className="p-field">
                        <div className="row"><label htmlFor="extension">Extension</label></div>
                        <input id="extension" type="text" value={newEmployee.extension || ''} onChange={(e) => setNewEmployee({ ...newEmployee, extension: e.target.value })} />
                    </div>
                    <div className="p-field">
                        <div className="row"><label htmlFor="roleId">Role</label></div>
                        <Dropdown id="roleId" value={newEmployee.roleId} options={roleOptions} onChange={(e) => setNewEmployee({ ...newEmployee, roleId: e.value })} optionLabel="name" placeholder="Select a Role" />
                    </div>
                </div>
                <Button label="Cancel" className="p-button-secondary" onClick={() => setDisplayDialog(false)} />
                <Button label="Save" className="p-button-primary" onClick={addEmployee} />
            </Dialog>

        </div>
    );
}
