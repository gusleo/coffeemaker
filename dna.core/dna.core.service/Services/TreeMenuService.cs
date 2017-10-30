using dna.core.data.Entities;
using dna.core.service.Models;
using dna.core.service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dna.core.data.Infrastructure;
using dna.core.service.Infrastructure;
using dna.core.data.UnitOfWork;
using AutoMapper;
using dna.core.libs.TreeMenu;
using dna.core.auth;

namespace dna.core.service.Services
{
    public class TreeMenuService : ReadWriteServiceBase<TreeMenuModel, TreeMenu>, ITreeMenuService
    {
        protected readonly IDNAUnitOfWork _unitOfWork;
        public TreeMenuService(IAuthenticationService authService, IDNAUnitOfWork unitOfWork) : base(authService)
        {
            _unitOfWork = unitOfWork;
        }
        public Response<TreeMenuModel> Create(TreeMenuModel modelToCreate)
        {
            var response = InitErrorResponse();
            if ( Validate(modelToCreate) )
            {
                try
                {
                    var en = GetEntityFromModel(modelToCreate);
                    _unitOfWork.TreeMenuRepository.Add(en);
                    _unitOfWork.Commit();
                    response = InitSuccessResponse(MessageConstant.Create);
                    response.Item = GetModelFromEntity(en);
                }
                catch(Exception ex )
                {
                    response.Message = ex.Message;
                }
            }
            else
            {
                response.Message = MessageConstant.ValidationError;
            }
            return response;
        }

        public async Task<Response<TreeMenuModel>> Delete(int id)
        {
            var response = InitErrorResponse();            
            try
            {
                var en = await _unitOfWork.TreeMenuRepository.GetSingleAsync(id);
                _unitOfWork.TreeMenuRepository.Delete(en);
                _unitOfWork.Commit();
                response = InitSuccessResponse(MessageConstant.Delete);
                response.Item = GetModelFromEntity(en);
            }
            catch ( Exception ex )
            {
                response.Message = ex.Message;
            }
            return response;
            
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public Response<TreeMenuModel> Edit(TreeMenuModel modelToEdit)
        {
            var response = InitErrorResponse();
            if ( Validate(modelToEdit) )
            {
                try
                {
                    var en = GetEntityFromModel(modelToEdit);
                    _unitOfWork.TreeMenuRepository.Edit(en);
                    _unitOfWork.Commit();
                    response = InitSuccessResponse(MessageConstant.Update);
                    response.Item = GetModelFromEntity(en);
                }
                catch ( Exception ex )
                {
                    response.Message = ex.Message;
                }
            }else
            {
                response.Message = MessageConstant.ValidationError;
            }
            return response;
        }

        public async Task<Response<PaginationSet<TreeMenuModel>>> GetAllAsync(int pageIndex, int pageSize = 20)
        {
            var response = InitErrorResponse(pageIndex, pageSize);
            try
            {
                var result = await _unitOfWork.TreeMenuRepository.GetAllAsync(pageIndex, pageSize);
                response = InitSuccessResponse(pageIndex, pageSize, MessageConstant.Load);
                response.Item = Mapper.Map<PaginationSet<TreeMenuModel>>(result);
            }catch(Exception ex )
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<IList<TreeMenuModel>>> GetAllParentMenuAsync(MenuType type)
        {
            var response = InitSuccessListResponse(MessageConstant.Load);
            var result = await _unitOfWork.TreeMenuRepository.FindByAsync(x => x.Type == type && x.ParentId == 0);
            response.Item = GetModelFromEntity(result.ToList());
            return response;
        }

        public async Task<Response<IList<MenuItem>>> GetMenuByTypeAsync(MenuType type)
        {
            string role = _authService.GetUserRole();
            var response = InitErrorListResponse<MenuItem>();
            try
            {
                var result = await _unitOfWork.TreeMenuRepository.FindByAsync(x => x.Type == type);

                IList<MenuItem> list = new List<MenuItem>();
                foreach(var item in result )
                {
                    MenuItem menu = new MenuItem
                    {
                        Id = item.Id,
                        Icon = item.DisplayIcon,
                        Name = item.DisplayName,
                        State = item.Link,
                        ParentId = item.ParentId,
                        Order = item.Order,
                        Roles = item.Roles.Split(',')
                    };
                    list.Add(menu);
                }

                var treeMenu = new TreeMenuBuilder().Build(role, list);
                response = InitSuccessListResponse<MenuItem>(MessageConstant.Load);
                response.Item = treeMenu;
            }
            catch ( Exception ex )
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<PaginationSet<TreeMenuModel>>> GetMenuByTypeAsync(MenuType type, int pageIndex, int pageSize)
        {
            var response = InitSuccessResponse(pageIndex, pageSize, MessageConstant.Load);
            var result = await _unitOfWork.TreeMenuRepository.FindByAsync(x => x.Type == type, pageIndex, pageSize);
            response.Item = GetModelFromEntity(result);
            return response;

        }

        public async Task<Response<TreeMenuModel>> GetSingleAsync(int id)
        {
            var response = InitErrorResponse();
            try
            {
                var en = await _unitOfWork.TreeMenuRepository.GetSingleAsync(id);                
                response = InitSuccessResponse(MessageConstant.Load);
                response.Item = GetModelFromEntity(en);

            }
            catch ( Exception ex )
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public TreeMenu RemoveChildEntity(TreeMenu entity)
        {
            return entity;
        }

        public bool Validate(TreeMenuModel modelToValidate)
        {
            return _validationDictionary.IsValid;
        }
    }
}
