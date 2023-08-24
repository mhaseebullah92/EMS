import React, { useState, useEffect } from "react";
import layout from "../layout/layout"
import HTTPService from "../services/HTTPService"
import { Button } from "react-bootstrap";
import AddEditStudentModal from "../modal/AddEditStudentModal"
import Form from 'react-bootstrap/Form';

const Students = ({ props }) => {

    const [students, setStudents] = useState([]);
    const [addStudentModal, setAddStudentModal] = useState({ show: false });
    const [keyWord, setKeyWord] = useState('');

    useEffect(() => {
        getStudents();
    }, []);

    const getStudents = async (keyWord) => {
        const response = await HTTPService.get(`/api/employee` + (keyWord ? `?keyWord=${keyWord}` : ``));
        if (response.succeeded) {
            if (response.data) {
                setStudents(response.data.map(item => {
                    return {
                        ...item,
                        dateOfBirth: new Date(item.dateOfBirth).toISOString().substr(0, 10)
                    }
                }));
            }
        }
    };

    const deleteStudents = async (id) => {
        const response = await HTTPService.del(`/api/employee/${id}`);
        if (response.succeeded) {
            if (response.data) {
                getStudents();
            }
        }
    };

    return <>
        <AddEditStudentModal props={addStudentModal} />
        <div className="row mt-5 justify-content-center">
            <div className="col-auto">
                <h3>Search: </h3>
            </div>
            <div className="col-auto">
                <Form.Control type="text" placeholder="name, email, department" onChange={(e) => {
                    setKeyWord(e.target.value);
                }} value={keyWord} />
            </div>
            <div className="col-auto">
                <Button className="btn btn-primary" onClick={() => {
                    getStudents(keyWord);
                }}>Search</Button>
            </div>
        </div>
        <div className="row mt-5">
            <div className="col">
                <h2>Students <Button className="btn btn-primary float-right" onClick={() => setAddStudentModal({
                    show: true, onClose: () => {
                        getStudents();
                    }
                })}>Add</Button></h2>
                <div className="table-responsive">
                    <table className="table table-striped table-sm">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Name</th>
                                <th>Email</th>
                                <th>Phone</th>
                                <th>Date of Birth</th>
                                <th>Department</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {students && students.length > 0 ? students.map((student) => <tr key={student.id}>
                                <td>{student.id}</td>
                                <td>{student.name}</td>
                                <td>{student.email}</td>
                                <td>{student.phone}</td>
                                <td>{student.dateOfBirth}</td>
                                <td>{student.department}</td>
                                <td><Button className="btn btn-primary" onClick={() => setAddStudentModal({
                                    show: true, onClose: () => {
                                        getStudents();
                                    }, student
                                })}>Edit</Button><Button className="btn btn-danger" onClick={() => {
                                    if (window.confirm('Are you sure you wish to delete this item?')) {
                                        deleteStudents(student.id);
                                    }
                                }}>Delete</Button></td>
                            </tr>) : <tr>
                                <td colSpan={7}>No record found</td>
                            </tr>}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

    </>
}

export default layout(Students);