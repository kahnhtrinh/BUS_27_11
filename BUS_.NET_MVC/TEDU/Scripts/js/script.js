document.getElementById('search-button').addEventListener('click', function () {
    // Giả lập kết quả tìm kiếm
    const schedules = [
        {
            time: "17:45",
            from: "Bến Xe An Nhơn, Bình Định",
            to: "Bến xe An Sương",
            duration: "14 giờ",
            vehicle: "Limousine",
            availableSeats: 18,
            price: "340.000đ"
        },
        {
            time: "18:45",
            from: "Bến Xe An Nhơn, Bình Định",
            to: "BX Miền Đông Mới",
            duration: "13 giờ",
            vehicle: "Limousine",
            availableSeats: 21,
            price: "340.000đ"
        }
    ];

    const scheduleList = document.getElementById('schedule-list');
    scheduleList.innerHTML = '';

    schedules.forEach(schedule => {
        const scheduleItem = document.createElement('div');
        scheduleItem.classList.add('schedule-item');

        scheduleItem.innerHTML = `
            <div class="info">
                <div><strong>${schedule.time}</strong> | ${schedule.duration}</div>
                <div>${schedule.from} -> ${schedule.to}</div>
                <div>${schedule.vehicle} | ${schedule.availableSeats} chỗ trống</div>
            </div>
            <div class="price">${schedule.price}</div>
            <button class="choose-btn">Chọn chuyến</button>
        `;

        scheduleList.appendChild(scheduleItem);
    });
});
