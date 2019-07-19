<template>
  <el-dialog :visible.sync="dialogFormVisible" title="对象编辑">
    <el-form ref="postForm" :model="postForm" :rules="rules" class="form-container">
      <el-form-item label="对象" prop="name">
        <el-input v-model="postForm.name" :maxlength="100" placeholder="必填" />
      </el-form-item>

      <el-form-item label="对象名称" prop="displayName">
        <el-input v-model="postForm.displayName" :maxlength="100" placeholder="必填" />
      </el-form-item>

      <el-form-item label="描述" prop="description">
        <el-input v-model="postForm.description" :maxlength="100" />
      </el-form-item>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="cancel">{{ $t('table.cancel') }}</el-button>
      <el-button type="primary" @click="submitForm">{{ $t('table.confirm') }}</el-button>
    </div>
  </el-dialog>
</template>

<script>
import edit from './edit.js'

export default {
  name: 'Edit',
  components: { },
  props: {
    dialogFormVisible: {
      type: Boolean,
      default: true
    }
  },
  data() {
    return {
      postForm: {},
      rules: {
        name: [{ required: true, message: '对象必填', trigger: 'blur' }],
        displayName: [{ required: true, message: '对象名称必填', trigger: 'blur' }]
      }
    }
  },
  computed: {
  },
  created() {
  },
  methods: {
    submitForm() {
      this.$refs.postForm.validate((valid) => {
        if (valid) {
          this.loading = true
          this.$notify({
            	title: '成功',
            	message: '发布文章成功',
            	type: 'success',
            	duration: 2000
          })
          this.loading = false
        } else {
          console.log('error submit!!')
          return false
        }
      })
    },
    cancel() {
      // 无法改变props的dialogFormVisible，通过emit通知父级页面刷新该值
      this.$emit('cancel')
    }
  }
}
</script>
