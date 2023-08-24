import { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import HTTPService from "../services/HTTPService"
import { ToastContainer, toast } from 'react-toastify';

const AddEditStudentModal = ({ props }) => {

    const [show, setShow] = useState(false);
    const [student, setStudent] = useState({
        id: '',
        name: '',
        email: '',
        phone: '',
        dateOfBirth: '',
        department: ''
    });
    const [edit, setEdit] = useState(false);
    const [validated, setValidated] = useState(false);

    const handleClose = () => {
        setShow(false);
        setValidated(false);
        setEdit(false);
        setStudent({ id: '', name: '', email: '', phone: '', dateOfBirth: '', department: '' })
    };

    const handleSubmit = async (event) => {
        const form = event.currentTarget;
        if (form.checkValidity() === true) {
            const response = edit ? await HTTPService.put(`/api/employee/${student.id}`, student) : await HTTPService.post(`/api/employee`, student);
            debugger;
            if (response.succeeded) {
                handleClose();
                props.onClose();
            } else {
                toast.error(response.message);
            }
          
        }
        event.preventDefault();
          event.stopPropagation();
        setValidated(true);
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
        <ToastContainer position="bottom-right" />
        <Modal show={show} onHide={handleClose}>
            <Form noValidate validated={validated} onSubmit={async (event) => {
                await handleSubmit(event);
            }}>
                <Modal.Header closeButton>
                    <Modal.Title>{edit ? 'Edit' : 'Add'} student</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group className="mb-3">
                        <Form.Label>Name</Form.Label>
                        <Form.Control type="text" placeholder="name" required onChange={(e) => {
                            setStudent({
                                ...student,
                                name: e.target.value
                            })
                        }} value={student.name} />
                        <Form.Control.Feedback type="invalid">
                            * required
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Email address</Form.Label>
                        <Form.Control type="email" placeholder="email" required onChange={(e) => {
                            setStudent({
                                ...student,
                                email: e.target.value
                            })
                        }} value={student.email} />
                        <Form.Control.Feedback type="invalid">
                            { student.email ? '* invalid value': '* required' }
                        </Form.Control.Feedback>
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
                        <Form.Control type="date" placeholder="date" required onChange={(e) => {
                            setStudent({
                                ...student,
                                dateOfBirth: e.target.value
                            })
                        }} value={student.dateOfBirth} />
                        <Form.Control.Feedback type="invalid">
                            * required
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Department</Form.Label>
                        <Form.Control type="type" placeholder="department" required onChange={(e) => {
                            setStudent({
                                ...student,
                                department: e.target.value
                            })
                        }} value={student.department} />
                        <Form.Control.Feedback type="invalid">
                            * required
                        </Form.Control.Feedback>
                    </Form.Group>

                </Modal.Body>
                <Modal.Footer>
                    <Button type='button' variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button type="submit" variant="primary">
                        Save
                    </Button>
                </Modal.Footer>
            </Form>
        </Modal>
    </>;
}

export default AddEditStudentModal;