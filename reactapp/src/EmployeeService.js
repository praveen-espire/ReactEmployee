export default class EmployeeService {
    async getEmployees() {
        try {
            const response = await fetch('https://localhost:7098/Employee');
            const data = await response.json();
            console.log(data.results);
            return data.results;
        } catch (error) {
            console.error('Error fetching employees:', error);
            throw error;
        }
    }

    async addEmployee(employee) {
        try {
            const response = await fetch('https://localhost:7098/Employee', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(employee)
            });
            const data = await response.json();
            console.log('Employee added:', data);
            return data; // Return the added employee or any response from the server
        } catch (error) {
            console.error('Error adding employee:', error);
            throw error;
        }
    }

    async deleteEmployee(employeeId) {
        try {
            const response = await fetch(`https://localhost:7098/Employee/${employeeId}`, {
                method: 'DELETE'
            });
            console.log(response);
            console.log('Employee deleted:', employeeId);
            // Assuming you don't need any response data after deletion
        } catch (error) {
            console.error('Error deleting employee:', error);
            throw error;
        }
    }
} 