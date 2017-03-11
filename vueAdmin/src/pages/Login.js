import { requestLogin } from '../api/api';
import NProgress from 'nprogress'
import axios from 'axios';
export default {
    data() {
        return {
            logining: false,
            ruleForm2: {
                account: 'admin',
                checkPass: '123456'
            },
            rules2: {
                account: [
                    { required: true, message: '请输入账号', trigger: 'blur' },
                    //{ validator: validaePass }
                ],
                checkPass: [
                    { required: true, message: '请输入密码', trigger: 'blur' },
                    //{ validator: validaePass2 }
                ]
            },
            checked: true
        };
    },
    methods: {

        test2() {

            axios.post('/a', {
                class: 'customer',
                para: 'getCustomerList',
                j: {
                   
                }

            }).then(function (response) {
                console.log(response);
            }).catch(function (error) {
                console.log(error);
            });

        },

        handleReset2() {
            this.$refs.ruleForm2.resetFields();
        },
        LoginIn(ev) {

           sessionStorage.setItem('user', JSON.stringify({username:"admin",name:" 王力",avatar:"/static/images/logo.png"}));
           this.$router.push({ path: '/main' });
            return;
            var _this = this;
            this.$refs.ruleForm2.validate((valid) => {
                if (valid) {
                    //_this.$router.replace('/table');
                    this.logining = true;
                    NProgress.start();
                    var loginParams = { username: this.ruleForm2.account, password: this.ruleForm2.checkPass };
                    requestLogin(loginParams).then(data => {
                        this.logining = false;
                        NProgress.done();
                        let { msg, code, user } = data;
                        if (code !== 200) {
                            this.$notify({
                                title: '错误',
                                message: msg,
                                type: 'error'
                            });
                        } else {
                            sessionStorage.setItem('user', JSON.stringify(user));
                            this.$router.push({ path: '/main' });
                        }
                    });
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });
        }
    }
}