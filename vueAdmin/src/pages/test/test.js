import util from '../../common/js/util'
import NProgress from 'nprogress'
import { getUserListPage, removeUser, batchRemoveUser, editUser, addUser } from '../../api/api';
import axios from 'axios';
import address from "../../../models/dict/address"
import moment from 'moment'
export default {
    data() {
        return {
            filters: {
                name: ''
            },
            customerList: [],
            total: 0,
            page: 1,
            listLoading: false,
            sels: [],//列表选中列

            editFormVisible: false,//编辑界面是否显示
            editLoading: false,
            editFormRules: {
                name: [
                    { required: true, message: '请输入姓名', trigger: 'blur' }
                ]
            },
            //编辑界面数据
            editForm: {
                id: 0,
                name: '',
                sex: -1,
                age: 0,
                birth: '',
                addr: ''
            },

            addFormVisible: false,//新增界面是否显示
            addLoading: false,
            ShowSave: false,
            addFormRules: {
                name: [
                    { required: true, message: '请输入姓名', trigger: 'blur' }
                ]
            },
            //新增界面数据
            addForm: {
                name: '',
                sex: -1,
                age: 0,
                birth: '',
                addr: ''
            },
            currObj: {


                address: {}
            },
            addressOptions: address.address,
            selectedOptions: ["330000", "330100", "330110"],
            totalPage: 1


        }
    },
    methods: {
        //性别显示转换
        formatSex: function (row, column) {
            return row.sex == 1 ? '男' : row.sex == 0 ? '女' : '未知';
        },
        fmtDate: function (row, col) {

            return moment(row.createTime).format("YYYY-MM-DD HH:mm:ss");

        },
        fmtaddress: (row, col) => {
            if (row.address) {

                return row.address.provinceName + " " + row.address.cityName
            }
            else {

                return "未知"
            }

        },
        addressChange: (value) => {

            var iii = value;

        },

        handleCurrentChange(val) {
            this.page = val;
            this.getCustomerList();
        },
        //获取用户列表
        getCustomerList() {

            this.listLoading = true;
            NProgress.start();
            axios.post("/a", {
                class: "customer",
                para: "getCustomerList",
                j: {}

            }).then((res) => {

                this.customerList = res.data.list;

                this.totalPage = res.data.totalPage;
                this.listLoading = false;
                NProgress.done();


            });
        },
        addCustomer() {

            this.currObj = {

                address: {}
            }
            this.ShowSave = true;
        },

        //删除
        handleDel: function (index, row) {

        },
        //显示编辑界面
        toSaveCustomer: function (index, row) {
            axios.post("/a", {
                class: "customer",
                para: "getCustomer",
                j: {
                    _id: this.customerList[index]._id
                }

            }).then((res) => {

                this.currObj = res.data.info;
                this.ShowSave = true;


                if (!this.currObj.address) {

                    this.currObj.address = {};
                    this.currObj.address.areaId = '';
                }



                this.selectedOptions = [this.currObj.address.provinceId, this.currObj.address.cityId, this.currObj.address.areaId]
            });

        },
        //保存客户
        saveCustomer: function () {

            this.currObj.address = address.addressObj(this.selectedOptions, this.currObj.address.memo);

            axios.post("/a", {
                class: "customer",
                para: "saveCustomer",
                j: this.currObj

            }).then((res) => {
                if (res.data.re == "ok") {
                    this.ShowSave = false;
                    this.getCustomerList();
                } else {
                    alert(JSON.stringify(res.data));
                }
            });


        }
        //新增





    },


    mounted() {
        this.getCustomerList();
    },

}