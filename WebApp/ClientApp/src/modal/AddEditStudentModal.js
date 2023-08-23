import { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import HTTPService from "../services/HTTPService"

const AddEditStudentModal = ({ props }) => {

    const [show, setShow] = useState(false);
    const [student, setStudent] = useState({ id: '', name: '', email: '', phone: '', dateOfBirth: '', department: '' });
    const [edit, setEdit] = useState(false);

    const handleClose = () => {
        setStudent({ id: '', name: '', email: '', phone: '', dateOfBirth: '', department: '' })
        setEdit(false);
        setShow(false);
    };

    const handleSubmit = async () => {
        const response = edit ? await HTTPService.put(`/api/employee/${student.id}`, student) : await HTTPService.post(`/api/employee`, student);
        if (response.succeeded) {
            handleClose();
            props.onClose();
        }
    };

    useEffect(() => {
        if (props) {
            setShow(props.show);
            if (props.student) {
                setStudent(props.student);
                setEdit(true);
            }
        }
    }, [props]);

    return <>
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>{edit ? 'Edit' : 'Add'} student</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group className="mb-3">
                        <Form.Label>Name</Form.Label>
                        <Form.Control type="text" placeholder="name" onChange={(e) => {
                            setStudent({
                                ...student,
                                name: e.target.value
                            })
                        }} value={student.name} />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Email address</Form.Label>
                        <Form.Control type="email" placeholder="email" onChange={(e) => {
                            setStudent({
                                ...student,
                                email: e.target.value
                            })
                        }} value={student.email} />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Phone</Form.Label>
                        <Form.Control type="text" placeholder="phone" onChange={(e) => {
                            setStudent({
                                ...student,
                                phone: e.target.value
                            })
                        }} value={student.phone} />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Date of birth</Form.Label>
                        <Form.Control type="date" placeholder="date" onChange={(e) => {
                            setStudent({
                                ...student,
                                dateOfBirth: e.target.value
                            })
                        }} value={student.dateOfBirth} />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Department</Form.Label>
                        <Form.Control type="type" placeholder="department" onChange={(e) => {
                            setStudent({
                                ...student,
                                department: e.target.value
                            })
                        }} value={student.department} />
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleClose}>
                    Close
                </Button>
                <Button variant="primary" onClick={handleSubmit}>
                    Save
                </Button>
            </Modal.Footer>
        </Modal>
    </>;
}

export default AddEditStudentModal;