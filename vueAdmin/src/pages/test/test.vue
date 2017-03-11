<template>
	<section>
		<!--工具条-->
		<el-col :span="24" class="toolbar" style="padding-bottom: 0px;">
			<el-form :inline="true" :model="filters">
				<el-form-item>
					<el-input v-model="filters.name" placeholder="姓名"></el-input>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" v-on:click="getCustomerList">查询</el-button>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" v-on:click="addCustomer">新增</el-button>
				</el-form-item>
			</el-form>
		</el-col>

		<!--列表-->
		<el-table :data="customerList" highlight-current-row v-loading="listLoading"  style="width: 100%;">

			<el-table-column prop="name" label="姓名" width="120" >
			</el-table-column>
			<el-table-column prop="tell" label="电话" width="100" >
			</el-table-column>
			<el-table-column prop="address" label="地址" width="200" :formatter="fmtaddress" >
			</el-table-column>
			<el-table-column prop="managerName" label="负责人" width="120" >
			</el-table-column>
			<el-table-column  label="创建时间" min-width="180" prop='createTime' :formatter="fmtDate"  >

			</el-table-column>
			<el-table-column label="操作" width="150">
				<template scope="scope">
					<el-button size="small" @click="toSaveCustomer(scope.$index, scope.row)">编辑</el-button>
					<el-button type="danger" size="small" @click="handleDel(scope.$index, scope.row)">删除</el-button>
				</template>
			</el-table-column>
		</el-table>

		<!--工具条-->
		<el-col :span="24" class="toolbar">
		
			<el-pagination layout="prev, pager, next" @current-change="handleCurrentChange" :page-size="20" :total="totalPage" style="float:right;">
			</el-pagination>
		</el-col>

		<!--编辑界面-->
		<el-dialog title="编辑" v-model="ShowSave" :close-on-click-modal="false">
			<el-form :model="currObj" label-width="80px" :rules="editFormRules" ref="currObj">
				<el-form-item label="姓名" prop="name">
					<el-input v-model="currObj.name" auto-complete="off"></el-input>
				</el-form-item>
				
			    <el-form-item label="电话" prop="tell">
					<el-input v-model="currObj.tell" auto-complete="off"></el-input>
				</el-form-item>
                	<el-form-item label="邮件" prop="email">
					<el-input v-model="currObj.email" auto-complete="off"></el-input>
				</el-form-item>
                <el-form-item label="网站" prop="website">
					<el-input v-model="currObj.website" auto-complete="off"></el-input>
				</el-form-item>
                <el-form-item label="网店" prop="shopsite">
					<el-input v-model="currObj.shopsite" auto-complete="off"></el-input>
				</el-form-item>
				<el-form-item label="地址">
                    <el-cascader  :options="addressOptions"  v-model="selectedOptions" :change='addressChange' ></el-cascader>
				</el-form-item>
				<el-form-item label="详细地址">
					<el-input type="textarea" v-model="currObj.address.memo"></el-input>
				</el-form-item>
                		<el-form-item label="备注">
					<el-input type="textarea" v-model="currObj.memo"></el-input>
				</el-form-item>

            
     
			</el-form>
			<div slot="footer" class="dialog-footer">
				<el-button @click.native="ShowSave = false">取消</el-button>
				<el-button type="primary" @click.native="saveCustomer" :loading="editLoading">提交</el-button>
			</div>
		</el-dialog>



	</section>
</template>
<script src="./test.js"></script>

<style lang="scss" scoped>

</style>