<template>
  <div class="app-container">

    <div class="filter-container">
      <el-input :placeholder="filter.search.placeholder" v-model="filter.search.searchText" style="width:300px;" class="filter-item" @keyup.enter.native="handleFilter"/>
      <el-button v-waves class="filter-item" type="primary" style="margin-left: 10px;" icon="el-icon-search" @click="handleFilter">{{ $t('table.search') }}</el-button>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">{{ $t('table.add') }}</el-button>
      <el-button v-waves :loading="downloadLoading" class="filter-item" type="primary" icon="el-icon-download" @click="handleDownload">{{ $t('table.export') }}</el-button>
    </div>

    <el-table v-loading="listLoading" :data="list" border fit highlight-current-row style="width: 100%">
      <el-table-column align="center" label="ID" width="80">
        <template slot-scope="scope">
          <span>{{ scope.row.ID }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Name">
        <template slot-scope="scope">
          <span>{{ scope.row.Name }}</span>
        </template>
      </el-table-column>

      <el-table-column label="DisplayName">
        <template slot-scope="scope">
          <span>{{ scope.row.DisplayName }}</span>
        </template>
      </el-table-column>

      <el-table-column width="180px" align="left" label="Date">
        <template slot-scope="scope">
          <span>{{ scope.row.Date | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
        </template>
      </el-table-column>

      <el-table-column width="120px" align="left" label="Author">
        <template slot-scope="scope">
          <span>{{ scope.row.Author }}</span>
        </template>
      </el-table-column>

      <el-table-column align="center" label="Actions" width="120">
        <template slot-scope="scope">
          <router-link :to="'/example/edit/'+scope.row.ID">
            <el-button type="primary" size="small" icon="el-icon-edit">Edit</el-button>
          </router-link>
        </template>
      </el-table-column>

    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <edit :dialog-form-visible="dialogFormVisible" @cancel="cancel"/>

  </div>
</template>

<script>
import { fetchList } from '@/api/modeling/object'
import Pagination from '@/components/Pagination' // Secondary package based on el-pagination
import edit from './object-edit'
import waves from '@/directive/waves' // Waves directive

export default {
  name: 'List',
  components: { Pagination, edit },
  directives: { waves },
  filters: {
  },
  data() {
    return {
      list: null,
      total: 0,
      dialogFormVisible: false,
      downloadLoading: false,
      listLoading: true,
      listQuery: {
        page: 1,
        limit: 20
      },
      filter: {
        search: {
          field: [
            { name: 'Name', displayName: '对象' },
            { name: 'DisplayName', displayName: '对象名称' }
          ],
          placeholder: '对象,对象名称',
          searchText: null
        }
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      fetchList(this.listQuery).then(response => {
        this.list = response.data.items
        this.total = response.data.total
        this.listLoading = false
      })
    },
    handleDownload() {
      this.downloadLoading = true
			import('@/vendor/Export2Excel').then(excel => {
			  const tHeader = ['Name', 'DisplayName']
			  const filterVal = ['Name', 'DisplayName']
			  const data = this.formatJson(filterVal, this.list)
			  excel.export_json_to_excel({
			    header: tHeader,
			    data,
			    filename: 'table-list'
			  })
			  this.downloadLoading = false
			})
    },
    handleSizeChange(val) {
      this.listQuery.limit = val
      this.getList()
    },
    handleCurrentChange(val) {
      this.listQuery.page = val
      this.getList()
    },
    handleFilter() {

    },
    handleCreate() {
      this.dialogFormVisible = true
      // this.$router.push({ path: this.redirect || '/modeling/object/create' })
    },
    handleDownload() {
    },
    cancel() {
      this.dialogFormVisible = false
    }
  }
}
</script>
