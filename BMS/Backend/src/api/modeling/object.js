import request from '@/utils/request'

export function fetchList(query) {
// 	var res = request({
// 		url: '/article/list',
// 		method: 'get',
// 		params: query
// 	});
// 	console.log(res);
// 	return res;

  var list = []
  for (var i = 0; i < 20; i++) {
    list.push({
      ID: i,
      Name: 'Product',
      DisplayName: '产品',
      Date: '2019-01-01',
      Author: '作者'
    })
  }

  var data = {
    total: 500,
    items: list
  }

  return new Promise(function(resolve, reject) {
    resolve({ data: data })
  })
//   return request({
//     url: '/article/list',
//     method: 'get',
//     params: query
//   })
}

export function fetchArticle(id) {
  return request({
    url: '/article/detail',
    method: 'get',
    params: { id }
  })
}

export function fetchPv(pv) {
  return request({
    url: '/article/pv',
    method: 'get',
    params: { pv }
  })
}

export function createArticle(data) {
  return request({
    url: '/article/create',
    method: 'post',
    data
  })
}

export function updateArticle(data) {
  return request({
    url: '/article/update',
    method: 'post',
    data
  })
}
