$(document).ready(function () {
    loadData();
    debugger
    loadsukien();
    $('#datetimeModal').modal("show");
});



function loadsukien() {

    var token = localStorage.getItem('jwt');
    var user = localStorage.getItem('username');

    $.ajax({
        url: 'http://localhost:5100/api/Event/getAllEvent',
        method: 'GET',
        data: { username: user },
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            debugger
            response.forEach(function (event) {
                var now = new Date();
                var startTime = new Date(event.startTime);

                var timeDiff = startTime - now;
                var daysDiff = Math.ceil(timeDiff / (1000 * 60 * 60 * 24));
                var listsk = $('#listsukien')
                listsk.empty();

                var scheduleItem = `
							<label for= "scheduleEndDate" class= "form-label"> ${event.title}<span >- Còn ${daysDiff} ngày</span ></label><br />
						`;

                $('#listsukien').append(scheduleItem);


            });

        },
        error: function () {

        }
    });

}
function loadData() {
    var token = localStorage.getItem('jwt');
    var user = localStorage.getItem('username');

    $.ajax({
        url: 'http://localhost:5100/api/Event/getTop12',
        method: 'GET',
        data: { username: user },
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            debugger
            var list = $('#listEvent');
            list.empty();

            if (response.length < 1) {
                var html = `<h4 class = "text-center">Không có sự kiên nào </h4>`;
                list.append(html);
            } else {
                response.forEach(function (event) {
                    var now = new Date();
                    var startTime = new Date(event.startTime);
                    var endTime = new Date(event.endTime);
                    var statusHtml = '';
                    if (now < startTime) {
                        statusHtml = '<div class="mb-2" style="color: gray"><i class="fa-solid fa-circle" style="color: gray"></i> Sắp diễn ra</div>';
                    }
                    if (now >= startTime && now <= endTime) {
                        statusHtml = '<div class="mb-2" style="color: green"><i class="fa-solid fa-circle" style="color: green"></i> Đang diễn ra</div>';
                    }
                    var html = `
                    <div class="col-12 col-md-4 col-xl-4">
                        <div class="course-box blog grid-blog">
                            <div class="blog-image mb-0">
                                <a href="/home/DetailEvent/${event.eventId}"><img class="img-fluid" src="${event.image}" alt="Post Image"></a>
                            </div>
                            <div class="course-content">
                             <div class = "d-block">
                              <span class="date"> <i class="fa-solid fa-clock"></i> <strong> Thời gian bắt đầu : ${event.startTime}</strong> </span>
                               <span class="date"> <i class="fa-solid fa-location-dot"></i> <strong>Địa điểm : ${event.location}</strong></span>
                               
                               </div>
                                <a  href="/home/DetailEvent/${event.eventId}"> <span class="course-title">${event.title}</span></a>
                                ${statusHtml}
                                <div class="row">
                                    <div class="col">
                                        <a href="/home/EditEvent/${event.eventId}" class="text-success">
                                            <i class="far fa-edit"></i> Sửa
                                        </a>
                                    </div>
                                    <div class="col text-end">
                                        <a class="text-danger btn-share"  data-sid="${event.eventId}">
                                            <i class="far fa-share-alt"></i> Share
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                `;
                    list.append(html);
                });
            }
        },
        error: function (xhr, status, error) {
            debugger
            window.location.href = '/login';
            var response = JSON.parse(xhr.responseText);
        }
    });
}
$(document).on('click', '.btn-share', function () {
    debugger
    var eid = $(this).data('sid')
    loadUser(eid);

    $('#shareTable').modal("show");
    
})


function loadUser(eid) {

    var token = localStorage.getItem('jwt');
    var user = localStorage.getItem('username');
    $.ajax({
        url: 'http://localhost:5100/api/User/getAllUser',
        method: 'GET',
        data: { username: user },
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            debugger
            var listsk = $('#eventTableBody')
            listsk.empty();
            response.forEach(function (user) {
               
                
                var row = `
                    <tr>
                        <td>${user.username}</td>
                        <td>${user.email} ngày</td>
                        <td>
						<a class="text-primary share_event" data-uid='${user.userId}' data-eid='${eid}'>
							Share
						</a>
					</td>
                    </tr>
                `;
                
                listsk.append(row);


            });

        },
        error: function () {

        }
    });
}
$(document).on('click', '.share_event', function () {
    debugger
    var token = localStorage.getItem('jwt');
    var eid = $(this).data('eid');
    var uid = $(this).data('uid');
    var data = {
        eid: eid,
        uid: uid
    }
    $.ajax({
        url: 'http://localhost:5100/api/Event/ShareEvent/'+eid+'/'+uid,
        method: 'GET',
        data: data,
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            debugger
            alert("Chia sẻ sự kiện thành công")
            $('#shareTable').modal("dispose");

        },
        error: function () {

        }
    });

});

$(document).on('click', '.btn-delete', function () {
    debugger
    var id = $(this).data('id');
    if (confirm("Bạn có muốn xoá sự kiện này không ?")) {
        $.ajax({

            type: 'Delete',
            url: 'http://localhost:5100/api/Event/' + id,
            success: function () {
                alert("Xoá thành công");
                loadData();
            },
            error: function () {
                alert("Xoá thất bại");
            }

        });
    }

   

});

