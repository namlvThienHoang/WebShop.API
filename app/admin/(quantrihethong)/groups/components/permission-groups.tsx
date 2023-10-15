"use client";
import { IFormProps } from "@/shared/model";
import { Modal } from "@/shared/components/modal";
import { Formik, Form } from "formik";
import { toast } from "react-toastify";
import { TanetInput, TanetSelectTreeCheck, TanetCheckbox, TanetTextArea, TanetSelect, SelectAsync, TanetFormDate, TanetCKEditor } from "@/lib";
import { useEffect, useState, useReducer } from "react";
import { array, number, object, ref, string, date } from "yup";
import { groupsServices } from "../services";
import { formReducer, INITIAL_STATE_FORM, computedTitle } from "@/lib/common";
export default function PermissionGroups({
  show,
  action,
  id,
  onClose,
  ...props
}) {

  return (
    <>
      <Modal show={show} size="xl" loading={false}>

        <Modal.Header onClose={onClose}>Phân quyền</Modal.Header>
        <Modal.Body nameClass="grid-cols-12">

        </Modal.Body>
        <Modal.Footer onClose={onClose}>
          <button
            data-modal-hide="large-modal"
            type="submit"
            className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
          >
            Lưu
          </button>
        </Modal.Footer>

      </Modal>
    </>
  );
}
