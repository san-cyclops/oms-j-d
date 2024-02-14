using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain;
using Data;
using Utility;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.ComponentModel;
using EntityFramework.Extensions;
using MoreLinq;
using System.Data.Common;

namespace Service
{

    /// <summary>
    /// Developed by C.S.Malluwawadu
    /// </summary>
    /// 
    public class UserPrivilegesService
    {
        ERPDbContext context = new ERPDbContext();


        public List<AutoGenerateInfo> GetTransactionByTypeId(List<AutoGenerateInfo> autoGenerateInfoListPrm, int type)
        {
            var qry = autoGenerateInfoListPrm.ToArray();

            foreach (var temp in qry)
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = autoGenerateInfoListPrm.Where(a => a.ModuleType.Equals(type) && a.AutoGenerateInfoID.Equals(temp.AutoGenerateInfoID)).FirstOrDefault();

                if (autoGenerateInfo != null)
                {
                    autoGenerateInfoListPrm.Remove(autoGenerateInfo);

                    autoGenerateInfo.IsAccess = true;
                    autoGenerateInfo.IsPause = true;
                    autoGenerateInfo.IsSave = true;
                    autoGenerateInfo.IsModify = true;
                    autoGenerateInfo.IsView = true;

                    autoGenerateInfoListPrm.Add(autoGenerateInfo);
                }
            }
            return autoGenerateInfoListPrm.OrderBy(a => a.ModuleType).ToList();
        }


        public List<AutoGenerateInfo> GetAllTransactions()
        {
            return context.AutoGenerateInfos.OrderBy(a => a.ModuleType).ToList();
        }

        
        public UserGroup GetUserGroupByName(string UserGroupName)
        {
            return context.UserGroup.Where(a => a.UserGroupName.Equals(UserGroupName) && a.IsDelete.Equals(false)).FirstOrDefault();
        }

        public UserGroup GetUserGroupByID(long UserGroupID) 
        {
            return context.UserGroup.Where(a => a.UserGroupID.Equals(UserGroupID) && a.IsDelete.Equals(false)).FirstOrDefault();
        }

        public UserMaster GetUserByName(string UserName)
        {
            return context.UserMaster.Where(a => a.UserName.Equals(UserName) && a.IsDelete.Equals(false)).Include(up=> up.UserPrivileges).FirstOrDefault();
        }

        public UserMaster GetUserByEmployeeCode(string employeeCode)  
        {
            return context.UserMaster.Where(a => a.EmployeeCode.Equals(employeeCode) && a.IsDelete.Equals(false)).Include(up => up.UserPrivileges).FirstOrDefault();
        }


        public void AddUserGroup(UserGroup existingUserGroup, List<AutoGenerateInfo> autoGenerateInfoList, bool updateAllUsers) 
        {
            var qry = autoGenerateInfoList.ToArray();
            
            context.UserGroup.Add(existingUserGroup);
            context.SaveChanges();

            foreach (var temp in qry)
            {
                UserGroupPrivileges UserGroupPrivilegesSave = new UserGroupPrivileges();

                UserGroupPrivilegesSave.TransactionRightsID = temp.AutoGenerateInfoID;
                UserGroupPrivilegesSave.UserGroupID = existingUserGroup.UserGroupID;
                UserGroupPrivilegesSave.TransactionTypeID = temp.ModuleType;
                UserGroupPrivilegesSave.IsAccess = temp.IsAccess;
                UserGroupPrivilegesSave.IsPause = temp.IsPause;
                UserGroupPrivilegesSave.IsSave = temp.IsSave;
                UserGroupPrivilegesSave.IsModify = temp.IsModify;
                UserGroupPrivilegesSave.IsView = temp.IsView;

                context.UserGroupPrivileges.Add(UserGroupPrivilegesSave);
                context.SaveChanges();
            }

            if (updateAllUsers)
            {
                var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@GroupID", Value=existingUserGroup.UserGroupID },
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ApplyUser", Value=1 },
                    };

                CommonService.ExecuteStoredProcedure("spUpdateUserGroup", parameter); 
            }
        }

        public void UpdateUserGroup(UserGroup existingUserGroup, List<AutoGenerateInfo> autoGenerateInfoList, bool updateAllUsers) 
        {
            var qry = autoGenerateInfoList.ToArray();

            foreach (var temp in qry)
            {
                context.UserGroupPrivileges.Update(cp => cp.UserGroupID.Equals(existingUserGroup.UserGroupID) && cp.TransactionRightsID.Equals(temp.AutoGenerateInfoID), cp => new UserGroupPrivileges
                {
                    IsAccess = temp.IsAccess,
                    IsPause = temp.IsPause,
                    IsSave = temp.IsSave,
                    IsModify = temp.IsModify,
                    IsView = temp.IsView,
                    ModifiedDate = Common.GetSystemDateWithTime(),
                    ModifiedUser = Common.LoggedUser,
                    DataTransfer = 0,
                });
            }

            if (updateAllUsers == true)
            {
                var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@GroupID", Value=existingUserGroup.UserGroupID },
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ApplyUser", Value=1 },
                    };

                CommonService.ExecuteStoredProcedure("spUpdateUserGroup", parameter); 
            }
        }

        public List<UserGroupPrivileges> AddInvUserGroupPrivileges(List<TransactionRights> Transaction,long groupID)
        {
            
            List<UserGroupPrivileges> userGroupPrivilegesList = new List<UserGroupPrivileges>();
            UserGroupPrivileges userGroupPrivilegesTemp = new UserGroupPrivileges();

            foreach (TransactionRights invTransactionTemp in Transaction)
            {
               userGroupPrivilegesTemp = new UserGroupPrivileges();
               userGroupPrivilegesTemp.TransactionRightsID = invTransactionTemp.TransactionRightsID;
               userGroupPrivilegesTemp.TransactionTypeID = invTransactionTemp.TransactionTypeID;
               userGroupPrivilegesTemp.IsAccess = invTransactionTemp.IsAccess;
               userGroupPrivilegesTemp.IsPause = invTransactionTemp.IsPause;
               userGroupPrivilegesTemp.IsSave = invTransactionTemp.IsSave;
               userGroupPrivilegesTemp.IsModify = invTransactionTemp.IsModify;
               userGroupPrivilegesTemp.IsView = invTransactionTemp.IsView;
               userGroupPrivilegesTemp.UserGroupID = groupID;

               userGroupPrivilegesList.Add(userGroupPrivilegesTemp);

            }

            return userGroupPrivilegesList;

           
        }

        public List<UserGroup> GetAllUserGroups()
        {
            return context.UserGroup.Where(c => c.IsDelete == false).ToList();

        }

        public List<UserMaster> GetAllUserAccounts()
        {
            return context.UserMaster.Where(c => c.IsDelete == false).ToList();

        }

        public List<AutoGenerateInfo> GetUserGroupPrivilegesByUserGroupId(UserGroup userGroup)
        {
            AutoGenerateInfo autoGenerateInfo;

            List<AutoGenerateInfo> TransactionList = new List<AutoGenerateInfo>();


            var userGroupPrivilegesDetails = (from up in context.UserGroupPrivileges
                                      join tg in context.AutoGenerateInfos on up.TransactionRightsID equals tg.AutoGenerateInfoID
                                      where up.UserGroupID.Equals(userGroup.UserGroupID)
                                      select new  
                                      {                                   
                                          up.UserGroupID,
                                          tg.FormText,
                                          up.TransactionRightsID,
                                          up.IsAccess,
                                          up.IsPause,
                                          up.IsSave,
                                          up.IsModify,
                                          up.IsView,
                                          up.TransactionTypeID,
                                          up.UserGroupPrivilegesID,
                                          tg.FormId,
                                          tg.ModuleType,
                                          tg.AutoGenerateInfoID

                                      }).ToArray();

            foreach (var tempPrivileges in userGroupPrivilegesDetails)
            {

                autoGenerateInfo = new AutoGenerateInfo();

                autoGenerateInfo.FormText = tempPrivileges.FormText;
                autoGenerateInfo.ModuleType = tempPrivileges.ModuleType;
                autoGenerateInfo.IsAccess = tempPrivileges.IsAccess;
                autoGenerateInfo.IsPause = tempPrivileges.IsPause;
                autoGenerateInfo.IsSave = tempPrivileges.IsSave;
                autoGenerateInfo.IsModify = tempPrivileges.IsModify;
                autoGenerateInfo.IsView = tempPrivileges.IsView;
                autoGenerateInfo.FormId = tempPrivileges.FormId;
                autoGenerateInfo.AutoGenerateInfoID = tempPrivileges.AutoGenerateInfoID;

                TransactionList.Add(autoGenerateInfo);

            }
            return TransactionList.OrderBy(pd => pd.ModuleType).ToList();
        }


        public List<AutoGenerateInfo> GetUserGroupPrivileges(UserGroup userGroup, long masterID)
        {
            AutoGenerateInfo autoGenerateInfo;
            List<AutoGenerateInfo> autoGenerateInfoList = new List<AutoGenerateInfo>();


            var userGroupPrivilegesDetails = (from up in context.UserGroupPrivileges
                                              join tg in context.AutoGenerateInfos on up.TransactionRightsID equals tg.AutoGenerateInfoID
                                              where up.UserGroupID.Equals(userGroup.UserGroupID)
                                              select new
                                              {
                                                  up.UserGroupID,
                                                  tg.FormText,
                                                  up.TransactionRightsID,
                                                  up.IsAccess,
                                                  up.IsPause,
                                                  up.IsSave,
                                                  up.IsModify,
                                                  up.IsView,
                                                  up.TransactionTypeID,
                                                  up.UserGroupPrivilegesID,
                                                  tg.FormId,
                                                  tg.DocumentID,
                                                  tg.AutoGenerateInfoID

                                              }).ToArray();

            foreach (var tempPrivileges in userGroupPrivilegesDetails)
            {
                autoGenerateInfo = new AutoGenerateInfo();

                autoGenerateInfo.FormText = tempPrivileges.FormText;
                autoGenerateInfo.ModuleType = tempPrivileges.TransactionTypeID;
                autoGenerateInfo.IsAccess = tempPrivileges.IsAccess;
                autoGenerateInfo.IsPause = tempPrivileges.IsPause;
                autoGenerateInfo.IsSave = tempPrivileges.IsSave;
                autoGenerateInfo.IsModify = tempPrivileges.IsModify;
                autoGenerateInfo.IsView = tempPrivileges.IsView;
                autoGenerateInfo.FormId = tempPrivileges.FormId;
                autoGenerateInfo.AutoGenerateInfoID = tempPrivileges.AutoGenerateInfoID;
                
                autoGenerateInfoList.Add(autoGenerateInfo);
            }

            return autoGenerateInfoList.OrderBy(pd => pd.ModuleType).ToList();
        }


        public UserMaster GetUserPrivilegesByUser(string User)
        {
            return context.UserMaster.Where(a => a.UserName.Equals(User) && a.IsDelete.Equals(false)).FirstOrDefault();
        }


        public void AddUser(UserMaster User, List<Location> selectedLocations, List<AutoGenerateInfo> autoGenerateInfoPrm) 
        {
            User.UserPrivilegesLocations = null;
            User.UserPrivileges = null;

            context.UserMaster.Add(User);
            context.SaveChanges();

            var qry = autoGenerateInfoPrm.ToArray();

            foreach (var temp in qry)
            {
                UserPrivileges userPrivilegesSave = new UserPrivileges();

                userPrivilegesSave.UserMasterID = User.UserMasterID;
                userPrivilegesSave.TransactionRightsID = temp.AutoGenerateInfoID;
                userPrivilegesSave.FormID = temp.FormId;
                userPrivilegesSave.IsAccess = temp.IsAccess;
                userPrivilegesSave.IsPause = temp.IsPause;
                userPrivilegesSave.IsSave = temp.IsSave;
                userPrivilegesSave.IsModify = temp.IsModify;
                userPrivilegesSave.IsView = temp.IsView;

                context.UserPrivileges.Add(userPrivilegesSave);
                context.SaveChanges();
            }

            UserPrivilegesLocations uPLocations = new UserPrivilegesLocations();
           
            var locationQry = selectedLocations.ToArray();
            foreach (var tempLocation in locationQry)
            {

                UserPrivilegesLocations uPLocationsInsert = new UserPrivilegesLocations();
                uPLocationsInsert.UserMasterID = User.UserMasterID;
                uPLocationsInsert.UserGroupID = User.UserGroupID;
                uPLocationsInsert.LocationID = tempLocation.LocationID;
                uPLocationsInsert.IsSelect = tempLocation.IsSelect;

                context.UserPrivilegesLocations.Add(uPLocationsInsert);
                context.SaveChanges();
            }

        }


        public void UpdateUser(UserMaster existingUser, List<Location> selectedLocations, List<AutoGenerateInfo> autoGenerateInfo)
        {
            var qry = autoGenerateInfo.ToArray();

            //existingUser.UserPrivileges = null;
            //existingUser.UserPrivilegesLocations = null;

            context.UserMaster.Update(cp => cp.UserMasterID.Equals(existingUser.UserMasterID), cp => new UserMaster
            {
                ModifiedUser = Common.LoggedUser,
                ModifiedDate = Common.GetSystemDateWithTime(),
                DataTransfer = 0,
                UserGroupID = existingUser.UserGroupID,
                Password = existingUser.Password,
                UserDescription = existingUser.UserDescription,
                IsActive = existingUser.IsActive,
                IsUserCantChangePassword = existingUser.IsUserCantChangePassword,
                IsUserMustChangePassword = existingUser.IsUserMustChangePassword
            });

            foreach (var temp in qry)
            {
                context.UserPrivileges.Update(cp => cp.UserMasterID.Equals(existingUser.UserMasterID) && cp.TransactionRightsID.Equals(temp.AutoGenerateInfoID), cp => new UserPrivileges
                {
                    IsAccess=temp.IsAccess,
                    IsPause=temp.IsPause,
                    IsSave=temp.IsSave,
                    IsModify=temp.IsModify,
                    IsView=temp.IsView,
                    DataTransfer = 0,
                });
            }

            UserPrivilegesLocations uPLocations = new UserPrivilegesLocations();
           
            var locationQry = selectedLocations.ToArray();
            foreach (var tempLocation in locationQry)
            {
                uPLocations = context.UserPrivilegesLocations.Where(up => up.LocationID.Equals(tempLocation.LocationID) && up.UserMasterID.Equals(existingUser.UserMasterID)).FirstOrDefault();
                if (uPLocations == null)
                {
                    UserPrivilegesLocations uPLocationsInsert = new UserPrivilegesLocations();
                    uPLocationsInsert.UserMasterID = existingUser.UserMasterID;
                    uPLocationsInsert.UserGroupID = existingUser.UserGroupID;
                    uPLocationsInsert.LocationID = tempLocation.LocationID;
                    uPLocationsInsert.IsSelect = tempLocation.IsSelect;

                    context.UserPrivilegesLocations.Add(uPLocationsInsert);
                    context.SaveChanges();

                }

                else
                {

                    context.UserPrivilegesLocations.Update(cp => cp.UserMasterID.Equals(existingUser.UserMasterID) && cp.LocationID.Equals(tempLocation.LocationID), cp => new UserPrivilegesLocations
                    {
                        UserGroupID = existingUser.UserGroupID,
                        IsSelect = tempLocation.IsSelect,

                        DataTransfer = 0,
                    });
                }
            }

        }


        public List<AutoGenerateInfo> GetUserPrivilegesByUserId(UserMaster user)
        {
            AutoGenerateInfo autoGenerateInfo;

            List<AutoGenerateInfo> transactionList = new List<AutoGenerateInfo>();


            var userPrivilegesDetails = (from up in context.UserMaster
                                         join tg in context.UserPrivileges on up.UserMasterID equals tg.UserMasterID
                                         join ag in context.AutoGenerateInfos on tg.TransactionRightsID equals ag.AutoGenerateInfoID
                                         where up.UserMasterID.Equals(user.UserMasterID)
                                              select new
                                              {
                                                  up.UserMasterID,
                                                  up.UserGroupID,
                                                  up.UserName,
                                                  up.UserDescription,
                                                  up.Password,
                                                  up.IsUserCantChangePassword,
                                                  up.IsActive,
                                                  up.IsUserMustChangePassword,
                                                  tg.IsAccess,
                                                  tg.IsModify,
                                                  tg.IsView,
                                                  tg.IsSave,
                                                  tg.IsPause,
                                                  tg.UserPrivilegesID,
                                                  ag.FormText,
                                                  ag.AutoGenerateInfoID,
                                                  ag.ModuleType,
                                                  ag.FormId


                                              }).ToArray();

            foreach (var tempPrivileges in userPrivilegesDetails)
            {

                autoGenerateInfo = new AutoGenerateInfo();

                autoGenerateInfo.IsAccess = tempPrivileges.IsAccess;
                autoGenerateInfo.IsPause = tempPrivileges.IsPause;
                autoGenerateInfo.IsSave = tempPrivileges.IsSave;
                autoGenerateInfo.IsModify = tempPrivileges.IsModify;
                autoGenerateInfo.IsView = tempPrivileges.IsView;
                autoGenerateInfo.FormText = tempPrivileges.FormText;
                autoGenerateInfo.AutoGenerateInfoID = tempPrivileges.AutoGenerateInfoID;
                autoGenerateInfo.ModuleType = tempPrivileges.ModuleType;
                autoGenerateInfo.FormId = tempPrivileges.FormId;

                transactionList.Add(autoGenerateInfo);

            }
            return transactionList.OrderBy(pd => pd.ModuleType).ToList();
        }



        public List<UserPrivileges> GetAccessPrivilegesByUserId(long userID)
        {
            UserPrivileges existingUser;

            List<UserPrivileges> transactionList = new List<UserPrivileges>();


            var userPrivilegesDetails = (from tg in context.UserPrivileges
                                         join ai in context.AutoGenerateInfos on tg.FormID equals ai.FormId
                                         where tg.UserMasterID.Equals(userID) && tg.IsAccess.Equals(true)
                                         select new
                                         {
                                             tg.UserMasterID,
                                             tg.IsAccess,
                                             tg.IsModify,
                                             tg.IsView,
                                             tg.IsSave,
                                             tg.IsPause,
                                             tg.UserPrivilegesID,
                                             tg.TransactionTypeID,
                                             tg.FormID,
                                             ai.FormName


                                         }).ToArray();

            foreach (var tempPrivileges in userPrivilegesDetails)
            {

                existingUser = new UserPrivileges();


                existingUser.UserMasterID = tempPrivileges.UserMasterID;
                existingUser.IsAccess = tempPrivileges.IsAccess;
                existingUser.IsPause = tempPrivileges.IsPause;
                existingUser.IsSave = tempPrivileges.IsSave;
                existingUser.IsModify = tempPrivileges.IsModify;
                existingUser.IsView = tempPrivileges.IsView;
                existingUser.FormName = tempPrivileges.FormName;
                existingUser.FormID = tempPrivileges.FormID;


                transactionList.Add(existingUser);

            }


            return transactionList.OrderBy(pd => pd.TransactionTypeID).ToList();
        }


        public UserMaster getUserMasterByUserName(string userName)
        {
            return context.UserMaster.Where(ph => ph.UserName.Equals(userName) && ph.IsActive == true).FirstOrDefault();
        }

        public UserMaster getUserMasterByUserEmployeeCode(string employeeCode)  
        {
            return context.UserMaster.Where(ph => ph.EmployeeCode.Equals(employeeCode) && ph.IsActive == true).FirstOrDefault();
        }


        //public List<UserPrivilegesLocations> GetUserLocationsByUserId(UserMaster user)
        //{
        //    UserPrivilegesLocations existingUserLocations;

        //    List<UserPrivilegesLocations> locationList = new List<UserPrivilegesLocations>();


        //    var userLocationDetails = (from up in context.UserMaster
        //                               join tg in context.UserPrivilegesLocations on up.UserMasterID equals tg.UserMasterID
        //                               join lc in context.Locations on tg.LocationID equals lc.LocationID
        //                               where up.UserMasterID.Equals(user.UserMasterID) && up.UserGroupID == tg.UserGroupID
        //                               select new
        //                               {
        //                                   up.UserMasterID,
        //                                   tg.LocationID,
        //                                   tg.IsSelect,
        //                                   lc.LocationName,
        //                                   up.UserGroupID,
        //                                   tg.UserPrivilegesLocationsID

        //                               }).ToArray();


        //    foreach (var tempLocations in userLocationDetails)
        //    {

        //        existingUserLocations = new UserPrivilegesLocations();

        //        existingUserLocations.UserMasterID = tempLocations.UserMasterID;
        //        existingUserLocations.LocationID = tempLocations.LocationID;
        //        existingUserLocations.LocationName = tempLocations.LocationName;
        //        existingUserLocations.UserGroupID = tempLocations.UserGroupID;
        //        existingUserLocations.IsSelect = tempLocations.IsSelect;
        //        existingUserLocations.UserPrivilegesLocationsID = tempLocations.UserPrivilegesLocationsID;

        //        locationList.Add(existingUserLocations);

        //    }

        //    if (locationList.Count == 0)
        //    {
        //        var location = (from lc in context.Locations
        //                        select new
        //                        {

        //                            lc.LocationID,
        //                            lc.LocationName

        //                        }).ToArray();


        //        foreach (var tempLocations in location)
        //        {

        //            existingUserLocations = new UserPrivilegesLocations();

        //            existingUserLocations.UserMasterID = user.UserMasterID;
        //            existingUserLocations.LocationID = tempLocations.LocationID;
        //            existingUserLocations.LocationName = tempLocations.LocationName;
        //            existingUserLocations.UserGroupID = user.UserGroupID;
        //            existingUserLocations.IsSelect = false;

        //            locationList.Add(existingUserLocations);

        //        }
        //    }
        //    return locationList.OrderBy(pd => pd.LocationID).ToList();
        //}

        public List<Location> GetUserLocationsByUserId(UserMaster user)
        {
            List<Location> locationList = new List<Location>();

            var userLocationDetails = (from up in context.UserPrivilegesLocations
                                       join l in context.Locations on up.LocationID equals l.LocationID
                                       where up.UserMasterID == user.UserMasterID && l.IsDelete == false
                                       select new
                                       {
                                           up.UserMasterID,
                                           l.LocationID,
                                           up.IsSelect,
                                           l.LocationName,
                                           up.UserGroupID
                                       }).ToArray();


            foreach (var tempLocations in userLocationDetails)
            {
                Location location = new Location();
                LocationService locationService = new LocationService();

                if (tempLocations.LocationID == 29) 
                { 
                    continue;
                }

                location = locationService.GetLocationsByID(tempLocations.LocationID);
                location.IsSelect = tempLocations.IsSelect;

                locationList.Add(location);

            }

            if (locationList.Count == 0)
            {
                var location = (from lc in context.Locations
                                where lc.IsDelete == false
                                select new
                                {

                                    lc.LocationID,
                                    lc.LocationName

                                }).ToArray();


                foreach (var tempLocations in location)
                {
                    Location locationT = new Location();
                    LocationService locationService = new LocationService();

                    locationT = locationService.GetLocationsByID(tempLocations.LocationID);
                    locationT.IsSelect = false;

                    locationList.Add(locationT);

                }
            }
            return locationList.OrderBy(pd => pd.LocationID).ToList();
        }


        public void ChangeUserPassword(UserMaster userMasterPrm, string newPassword)
        {
            context.UserMaster.Update(um => um.UserMasterID.Equals(userMasterPrm.UserMasterID), cp => new UserMaster
            {
                Password = newPassword,
                IsUserMustChangePassword = false
            });
        }

        public void ChangeUserPasswordManually(UserMaster userMasterPrm, string newPassword) 
        {
            context.UserMaster.Update(um => um.UserMasterID.Equals(userMasterPrm.UserMasterID), cp => new UserMaster
            {
                Password = newPassword
            });
        }

        public DataTable GetAllActiveUsersDataTable(long userGroupID)
        {
            var qry = (from um in context.UserMaster
                       where um.UserGroupID == userGroupID
                       select new
                       {
                           um.UserName,
                           um.UserDescription
                       }).ToArray();
            return qry.ToDataTable();
        }


        public List<AutoGenerateInfo> RemoveTransactionByTypeId(List<AutoGenerateInfo> autoGenerateInfoListPrm, int type)
        {
            var qry = autoGenerateInfoListPrm.ToArray();

            foreach (var temp in qry)
            {
                AutoGenerateInfo autoGenerateInfo = new AutoGenerateInfo();
                autoGenerateInfo = autoGenerateInfoListPrm.Where(a => a.ModuleType.Equals(type) && a.AutoGenerateInfoID.Equals(temp.AutoGenerateInfoID)).FirstOrDefault();

                if (autoGenerateInfo != null)
                {
                    autoGenerateInfoListPrm.Remove(autoGenerateInfo);

                    autoGenerateInfo.IsAccess = false;
                    autoGenerateInfo.IsPause = false;
                    autoGenerateInfo.IsSave = false;
                    autoGenerateInfo.IsModify = false;
                    autoGenerateInfo.IsView = false;

                    autoGenerateInfoListPrm.Add(autoGenerateInfo);
                }
            }
            return autoGenerateInfoListPrm.OrderBy(a => a.ModuleType).ToList();
        }
    }
}
