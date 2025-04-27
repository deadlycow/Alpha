document.addEventListener('DOMContentLoaded', function () {

    updateRelativeTimes()
    updateNotificationCount()
})

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build()

connection.on("ReceiveNotification", function (notification) {
    const notificationContainer = document.querySelector('.notification-container');
    const notificationList = notificationContainer.querySelector('.notification-list');
    const newNotification = document.createElement('div');
    newNotification.classList.add('notification-item');
    newNotification.setAttribute('data-id', notification.id)
    newNotification.innerHTML =
        `
                <img class="notification-img" src="${notification.icon}" />
                <div>
                    <p>${notification.message}</p>
                    <span class="time" data-created="${new Date(notification.createdAt).toISOString()}">${notification.createdAt}</span>
                </div>
                <img class="remove" src="~/images/close-icon.svg" onclick="dismissNotification('${notification.id}')" />
        `
    notificationList.insertBefore(newNotification, notificationList.firstChild)
    updateRelativeTimes()
    updateNotificationCount()
});

connection.on("NotificationDismissed", function (notificationId) {
    removeNotification(notificationId)
})

connection.start().catch(error => console.log(error));

async function dismissNotification(notificationId) {
    try {
        const res = await fetch(`/api/notification/dismiss/${notificationId}`, { method: 'POST' })
        if (res.ok) {
            removeNotification(notificationId)
        }
        else {
            console.error('Failed to dismiss notification')
        }
    }
    catch (error) {
        console.log('Error removing notification: ', error)
    }
}

function removeNotification(notificationId) {
    const element = document.querySelector(`.notification-item[data-id="${notificationId}"]`)
    if (element) {
        element.remove()
        updateNotificationCount()
    }
}

function updateNotificationCount() {
    const notifications = document.querySelector('.notification-container')
    const notificationNumber = document.querySelector('.not-count')
    const notificationIcon = document.querySelector('#not-bel-dot')
    const count = notifications.querySelectorAll('.notification-item').length

    if (notificationNumber) {
        notificationNumber.textContent = count
    }

    if (count > 0 && !notificationIcon.classList.contains('new-note')) {
        notificationIcon.classList.add('new-note')
    }
    else if (count === 0 && notificationIcon.classList.contains('new-note')) {
        notificationIcon.classList.remove('new-note')
    }
}


//function updateRelativeTimes() {
//    const elements = document.querySelectorAll('.time');
//    const now = new Date();

//    elements.forEach(el => {
//        const created = new Date(el.getAttribute('data-created'));
//        const diff = now - created;
//        const diffSeconds = Math.floor(diff / 1000);
//        const diffMinutes = Math.floor(diffSeconds / 60);
//        const diffHours = Math.floor(diffMinutes / 60);
//        const diffDays = Math.floor(diffHours / 24);
//        const diffWeeks = Math.floor(diffDays / 7);

//        let relativeTime = '';

//        if (diffMinutes < 1) {
//            relativeTime = '0 min ago';
//        } else if (diffMinutes < 60) {
//            relativeTime = diffMinutes + ' min ago';
//        } else if (diffHours < 2) {
//            relativeTime = diffHours + ' hour ago';
//        } else if (diffHours < 24) {
//            relativeTime = diffHours + ' hours ago';
//        } else if (diffDays < 2) {
//            relativeTime = diffDays + ' day ago';
//        } else if (diffDays < 7) {
//            relativeTime = diffDays + ' days ago';
//        } else {
//            relativeTime = diffWeeks + ' weeks ago';
//        }

//        el.textContent = relativeTime;
//    });
//}
function updateRelativeTimes() {
    const elements = document.querySelectorAll('.time');
    const now = Date.now();

    elements.forEach(el => {
        const created = new Date(el.getAttribute('data-created')).getTime();
        const diffSeconds = Math.floor((now - created) / 1000);

        let relativeTime =
            diffSeconds < 60 ? '0 min ago' :
                diffSeconds < 3600 ? `${Math.floor(diffSeconds / 60)} min ago` :
                    diffSeconds < 7200 ? '1 hour ago' :
                        diffSeconds < 86400 ? `${Math.floor(diffSeconds / 3600)} hours ago` :
                            diffSeconds < 172800 ? '1 day ago' :
                                diffSeconds < 604800 ? `${Math.floor(diffSeconds / 86400)} days ago` :
                                    `${Math.floor(diffSeconds / 604800)} weeks ago`;

        el.textContent = relativeTime;
    });
}