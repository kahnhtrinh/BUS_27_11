
// Dữ liệu mẫu (có thể thay bằng API nếu cần)
const newsData = [
    {
        img: 'https://busmap.vn/wp-content/uploads/2023/03/IMG_2588.jpg',
        title: 'Nâng cấp giường nằm dòng xe Limousine',
        description: 'Dòng xe Limousine được nâng cấp với nhiều tính năng hiện đại.',
        link: '/tin-tuc/chi-tiet-tin-tuc/'
    },
    {
        img: 'https://mhcorp.vn/wp-content/uploads/2022/07/school-bus-system-bg-3.jpeg',
        title: 'Ra mắt dòng xe giường nằm hiện đại',
        description: 'Chiếc xe mang lại sự thoải mái cho mọi hành khách.',
        link: '/tin-tuc/chi-tiet-tin-tuc/'
    },
    {
        img: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRa9_OfDku3Qscjbv0aKlCzIYh2DzUddK8rYQ&s',
        title: 'Mở rộng tuyến đường Hà Nội - TP. HCM',
        description: 'Nhà xe mở rộng tuyến đường phục vụ hành khách xuyên Việt.',
        link: '/tin-tuc/chi-tiet-tin-tuc/'
    },
    {
        img: 'https://lilystravelagency.com/wp-content/uploads/2023/02/bus-tour.jpg',
        title: 'Dịch vụ đưa đón tận nơi',
        description: 'Dịch vụ tiện lợi, đáp ứng mọi nhu cầu của khách hàng.',
        link: '/tin-tuc/chi-tiet-tin-tuc/'
    },
    {
        img: 'https://lp-cms-production.imgix.net/2024-07/GettyImages-1461674120.jpg?w=1440&h=810&fit=crop&auto=format&q=75',
        title: 'Xe buýt công nghệ cao',
        description: 'Ứng dụng công nghệ mới nhất trong ngành vận tải.',
        link: '/tin-tuc/chi-tiet-tin-tuc/'
    },
    {
        img: 'https://mhcorp.vn/wp-content/uploads/2022/07/school-bus-system-bg-3.jpeg',
        title: 'Xe buýt công nghệ cao',
        description: 'Ứng dụng công nghệ mới nhất trong ngành vận tải.',
        link: '/tin-tuc/chi-tiet-tin-tuc/'
    },
    {
        img: 'https://mhcorp.vn/wp-content/uploads/2022/07/school-bus-system-bg-3.jpeg',
        title: 'Dịch vụ đưa đón tận nơi',
        description: 'Dịch vụ tiện lợi, đáp ứng mọi nhu cầu của khách hàng.',
        link: '/tin-tuc/chi-tiet-tin-tuc/'
    },
    {
        img: 'https://busmap.vn/wp-content/uploads/2023/03/IMG_2588.jpg',
        title: 'Xe buýt công nghệ cao',
        description: 'Ứng dụng công nghệ mới nhất trong ngành vận tải.',
        link: '/tin-tuc/chi-tiet-tin-tuc/'
    },
    {
        img: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRa9_OfDku3Qscjbv0aKlCzIYh2DzUddK8rYQ&s',
        title: 'Xe buýt công nghệ cao',
        description: 'Ứng dụng công nghệ mới nhất trong ngành vận tải.',
        link: '/tin-tuc/chi-tiet-tin-tuc/'
    }
];

const itemsPerPage = 3;
let currentPage = 1;

// Hàm hiển thị tin tức dựa trên trang
function displayNews(page) {
    const newsContainer = document.getElementById('news-container');
    const startIndex = (page - 1) * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;
    const pageItems = newsData.slice(startIndex, endIndex);

    newsContainer.innerHTML = ''; // Xóa nội dung cũ

    pageItems.forEach(news => {
        const newsHTML = `
            <div class="col-md-4">
                <div class="d-flex bg-white shadow rounded p-3">
                    <img  src="${news.img}" alt="" class="news-img me-3">
                    <div>
                        <h5 class="mb-2"><a href="${news.link}" class="text-dark">${news.title}</a></h5>
                        <p class="text-muted">${news.description}</p>
                    </div>
                </div>
            </div>
        `;
        newsContainer.innerHTML += newsHTML;
    });
}


// Hàm hiển thị phân trang
function setupPagination() {
    const pagination = document.getElementById('pagination');
    const totalPages = Math.ceil(newsData.length / itemsPerPage);

    pagination.innerHTML = ''; // Xóa nút phân trang cũ

    for (let i = 1; i <= totalPages; i++) {
        const isActive = i === currentPage ? 'active' : '';
        const pageButton = `
            <li class="page-item ${isActive}">
                <button class="page-link" onclick="changePage(${i})">${i}</button>
            </li>
        `;
        pagination.innerHTML += pageButton;
    }
}

// Hàm chuyển trang
function changePage(page) {
    currentPage = page;
    displayNews(page);
    setupPagination();
}

// Khởi tạo trang tin tức
displayNews(currentPage);
setupPagination();

