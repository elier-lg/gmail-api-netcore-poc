import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import Sidebar from '../../components/sideBar';
import EmailsList from './emailsList';
import Email from './email';

class Core extends React.Component {
  render() {
    return (
      <>
        <Sidebar />
        <Routes>
          <Route path="/" element={<EmailsList />} />
          <Route path=":emailId" element={<Email />} />
          <Route path="*" element={<Navigate to="/public/not-found" />} />
        </Routes>
      </>
    );
  }
}

export default Core;
