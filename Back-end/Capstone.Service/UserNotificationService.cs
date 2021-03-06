﻿using Capstone.Data.Infrastructrure;
using Capstone.Data.Repositories;
using Capstone.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.Service
{
    public interface IUserNotificationService
    {
        IEnumerable<UserNotification> GetAll();
        UserNotification GetByID(Guid ID);
        void Create(UserNotification userNotification);
        void Delete(UserNotification userNotification);
        void Save();
    }

    public class UserNotificationService : IUserNotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserNotificationRepository _userNotificationRepository;

        public UserNotificationService(IUnitOfWork unitOfWork, IUserNotificationRepository userNotificationRepository)
        {
            _unitOfWork = unitOfWork;
            _userNotificationRepository = userNotificationRepository;
        }

        public void Create(UserNotification userNotification)
        {
            _userNotificationRepository.Add(userNotification);
            _unitOfWork.Commit();
        }

        public void Delete(UserNotification userNotification)
        {
            _userNotificationRepository.Delete(userNotification);
            _unitOfWork.Commit();
        }

        public IEnumerable<UserNotification> GetAll()
        {
            return _userNotificationRepository.GetAll().Where(u => u.IsDeleted == false);
        }

        public UserNotification GetByID(Guid ID)
        {
            return _userNotificationRepository.GetById(ID);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
