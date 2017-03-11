import axios from 'axios';

let base = '';

export const requestLogin = params => {

    return axios.post(`${base}/login`, params).then(res => res.data);
};

export const getUserList = params => {
    return axios.get(`${base}/user/list`, { params: params });
};

export const getUserListPage = params => {



    return axios.get('/user/listpage', { params: params });
};

export const removeUser = params => {
    return axios.get(`${base}/user/remove`, { params: params });
};

export const batchRemoveUser = params => {
    return axios.get(`${base}/user/batchremove`, { params: params });
};

export const editUser = params => {

    alert(1);




    //return axios.get(`${base}/user/edit`, { params: params });
    axios.get("http://localhost:1337/static/js/address.json", {
        params: {
            class: "customer"
        }
    }).then(function (res) {

        console.log(res);
    }).catch(function (e) {
        console.log(e);

    })
   


     
};

export const addUser = params => {
    return axios.get(`${base}/user/add`, { params: params });
};



