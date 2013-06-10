using Coombu.Models;
using Coombu.ViewModels;
using CoombuPhoneApp.Models;
using CoombuPhoneApp.Models.ViewModels;
using CoombuPhoneApp.Utils;
using Spring.Http;
using Spring.Http.Client.Interceptor;
using Spring.Http.Converters;
using Spring.Http.Converters.Json;
using Spring.Rest.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;

namespace CoombuPhoneApp.Api
{
    public class CoombuApi
    {
        const string AUTH_TOKEN_HEADER_KEY = "Authorization-Token";
        const string AUTH_TOKEN_SETTING_KEY = "Token";
        const string DEFAULT_API_BASE_URL = "http://coombu134508.cloudapp.net/";
        private string _baseUrl;        
        private RestTemplate _template;
        private string _token;
        private IDictionary<string, string> _headers;

        private Boolean _isAuthenticated;
        
        #region Properties section
        public Boolean IsAuthenticated {
            get 
            {
                return _isAuthenticated;
            }
            set 
            {
                this._isAuthenticated = value;
            }
        }

        
        public string Token { 
            get 
            {                
                return _token;
            }
            set
            {
                if (value != _token)
                {
                    _headers[AUTH_TOKEN_HEADER_KEY] = value;
                    _token = value;
                    _isAuthenticated = true;

                    bool rememberMe = (bool) Settings.Get("RememberMe");

                    if (rememberMe)
                    {
                        Settings.Save(AUTH_TOKEN_SETTING_KEY , value);                        
                    }
                }                
            }
        }
        #endregion

        #region Constructor

        public CoombuApi()
        {
            _template = new RestTemplate(new Uri(DEFAULT_API_BASE_URL,UriKind.Absolute));
            
            this._baseUrl = DEFAULT_API_BASE_URL;
            InitHeaders();
            InitRestTemplate();            
        }

        public CoombuApi(string baseUrl)        
        {
            this._baseUrl = baseUrl;
            _template = new RestTemplate(new Uri(baseUrl,UriKind.Absolute));
            InitHeaders();
            InitRestTemplate();            
        }

        #endregion

        #region Initialization
        private void InitRestTemplate()
        {
            _template.MessageConverters.Add(new ResourceHttpMessageConverter());            
            //_template.MessageConverters.Add(new DataContractJsonHttpMessageConverter());
            _template.MessageConverters.Add(new NJsonHttpMessageConverter());
            _template.MessageConverters.Add(new FormHttpMessageConverter());
            _template.RequestInterceptors.Add(new CoombuHeaderInterceptor(this._headers));
        }

        private void InitHeaders()
        {
            this._headers = new Dictionary<string, string>();
            this._headers.Add("Content-Type","application/json");
            this._headers.Add("Accept", "application/json");
            
            //if(!string.IsNullOrEmpty(Token))
            //    this._headers.Add(AUTH_TOKEN_HEADER_KEY, this.Token);      
        }

        #endregion

        #region Helpers

        #endregion

        #region Authentication / Registration action

        public void Login(string username, string password,Action<UserToken> operationCompleted,bool rememberMe = false)
        {            
            HttpEntity entity = CreateEntity(new LoginModel { UserName = username, Password = password, RememberMe = rememberMe});
            try
            {
                Settings.Save("RememberMe" , rememberMe);                

                _template.ExchangeAsync<UserToken>("/api/auth",HttpMethod.POST, entity, r =>
                    {
                        if (r.Error == null)
                        {
                            UserToken userToken = r.Response.Body;
                            Token = userToken.token;
                                                        
                            operationCompleted(userToken);
                        }
                        else
                        {
                            Debug.WriteLine(r.Error.ToString());
                            operationCompleted(null);
                        }
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());                
            }
              
        }

        public void Register(RegisterModel profiles, Action<UserToken> operationCompleted)
        {
            UserToken userToken = null;
            HttpEntity entity = CreateEntity(profiles);
            _template.PostForObjectAsync<UserToken>("/register", entity, r =>
            {
                if (r.Error == null)
                {
                    this._token = userToken.token;
                    operationCompleted(r.Response);
                }
                else
                {
                    Debug.WriteLine(r.Error.InnerException);
                    Debug.WriteLine(r.Error.StackTrace);
                }
            });                       
        }

        #endregion

        #region Post action        

        public void GetAll(Action<List<Post>, int, int, int> operationCompleted, string username, int page = 1, int size = 25)
        {
            _template.GetForObjectAsync<ApiResponse<List<Post>>>("/api/posts?username={username}&page={page}&size={size}", r =>
            {
                if (r.Error == null)
                {
                    int currentPage = r.Response.Meta.ContainsKey("currentPage") ? int.Parse(r.Response.Meta["currentPage"].ToString()) : 0;
                    int totalPage = r.Response.Meta.ContainsKey("totalPage") ? int.Parse(r.Response.Meta["totalPage"].ToString()) : 0;

                    operationCompleted(r.Response.Data, currentPage, totalPage, size);
                }
                else
                {
                    Debug.WriteLine(r.Error.ToString());
                    Debug.WriteLine(r.Response);
                    operationCompleted(new List<Post>(), 0, 0, size);
                }
            },username, page, size);
        }

        public void GetAll(Action<List<Post>,int,int,int> operationCompleted, int page = 1 , int size = 25)
        {            
            _template.GetForObjectAsync<ApiResponse<List<Post>>>("/api/posts?page={page}&size={size}", r => 
            {
                if (r.Error == null)
                {
                    int currentPage = r.Response.Meta.ContainsKey("currentPage") ? int.Parse(r.Response.Meta["currentPage"].ToString()) : 0;
                    int totalPage = r.Response.Meta.ContainsKey("totalPage") ? int.Parse(r.Response.Meta["totalPage"].ToString()) : 0;

                    operationCompleted(r.Response.Data, currentPage, totalPage, size);
                }
                else
                {
                    Debug.WriteLine(r.Error.ToString());
                    Debug.WriteLine(r.Response);
                    operationCompleted(new List<Post>(),0,0,size);
                }
            }, page, size);            
        }

        public void GetPost(int id, Action<Post> operationCompleted)
        {                     
            _template.GetForObjectAsync<ApiResponse<Post>>("/api/posts/{id}", r => 
            {
                if (r.Error == null)
                {
                    operationCompleted(r.Response.Data);
                }
            },id);            
        }

        public void CreatePost(Post post, Action<Post> operationCompleted)
        {            
            _template.PostForObjectAsync<ApiResponse<Post>>("/api/posts", post,
                r => 
                {
                    if (r.Error == null)
                    {
                        operationCompleted(r.Response.Data);
                    }
                    else 
                    {
                        Debug.WriteLine(r.Error.ToString());
                        Debug.WriteLine(r.Response);
                        operationCompleted(null);
                    }
                });             
        }

        public void CreatePostWithPicture(Post post, string filename, Action<Post> operationCompleted)
        {            
            IDictionary<string, object> parts = new Dictionary<string, object>();
            
            parts.Add("Content", post.Content);
            parts.Add("Picture", new FileInfo(@filename));

            _template.PostForObjectAsync<ApiResponse<Post>>("/api/upload", parts,
                r =>
                {
                    if (r.Error == null)
                    {
                        operationCompleted(r.Response.Data);
                    }
                    else
                    {
                        operationCompleted(null);
                    }
                });
        }

        //public void UpdatePost(Post post, Action<Post> operationCompleted)
        //{
        //    HttpEntity entity = CreateEntity(post);

        //    _template.ExchangeAsync<Post>("/api/posts", HttpMethod.PUT, entity,
        //        r =>
        //        {
        //            if (r.Error == null)
        //            {
        //                operationCompleted(r.Response.Body);
        //            }
        //        });
            
            
        //}

        public void DeletePost(int id, Action<Boolean> operationCompleted)
        {
            HttpEntity entity = CreateEntity();

            _template.DeleteAsync("/api/posts/{id}", r => 
            {
                if (r.Error == null)
                {
                    operationCompleted(true);
                }
                else
                {
                    operationCompleted(false);
                }
            }, id);            
        }

        #endregion 
        

        #region Comment action

        public void CreateComment(int postId, Comment comment, Action<Comment> operationCompleted)
        {
            HttpEntity entity = CreateEntity(comment);
            _template.PostForObjectAsync<ApiResponse<Comment>>("/api/posts/{postId}/comments", entity, r =>
            {
                if (r.Error == null)
                {
                    operationCompleted(r.Response.Data);
                }
                else
                {
                    operationCompleted(null);
                }
            }, postId);
        }        

        public void DeleteComment(int postId,int commentId, Action<Boolean> operationCompleted)
        {
            HttpEntity entity = CreateEntity();
            _template.DeleteAsync("/api/posts/{postId}/comments/{commentId}", r =>
            {
                if (r.Error == null)
                {
                    operationCompleted(true);
                }
                else
                {
                    operationCompleted(false);
                }
            }, postId, commentId);
        }

        #endregion

        #region Group action 

        public void GetGroups(Action<List<Group>, int, int, int> operationCompleted, int page = 1, int size = 25)
        {
            _template.GetForObjectAsync<ApiResponse<List<Group>>>("/api/groups?page={page}&size={size}", r =>
            {
                if (r.Error == null)
                {
                    int currentPage = r.Response.Meta.ContainsKey("currentPage") ? int.Parse(r.Response.Meta["currentPage"].ToString()) : 0;
                    int totalPage = r.Response.Meta.ContainsKey("totalPage") ? int.Parse(r.Response.Meta["totalPage"].ToString()) : 0;

                    operationCompleted(r.Response.Data, currentPage, totalPage, size);
                }
                else
                {
                    Debug.WriteLine(r.Error.ToString());
                    operationCompleted(new List<Group>(), 0, 0, size);
                }
            }, page, size);

        }

        public void GetGroups(Action<List<Group>,int,int,int> operationCompleted, string username = null, int page = 1, int size = 25)
        {            
            _template.GetForObjectAsync<ApiResponse<List<Group>>>("/api/groups?username={username}&page={page}&size={size}", r => 
            {
                if (r.Error == null)
                {
                    int currentPage = r.Response.Meta.ContainsKey("currentPage") ? int.Parse(r.Response.Meta["currentPage"].ToString()) : 0;
                    int totalPage = r.Response.Meta.ContainsKey("totalPage") ? int.Parse(r.Response.Meta["totalPage"].ToString()) : 0;
                    operationCompleted(r.Response.Data,currentPage,totalPage,size);
                }
                else
                {
                    Debug.WriteLine(r.Error.ToString());
                    operationCompleted(new List<Group>(),0,0,size);
                }
            },username, page, size);
        
        }

        public void CreateGroup(Group group, Action<Group> operationCompleted)
        {            
            _template.PostForObjectAsync<ApiResponse<Group>>("/api/groups", group, r =>
            {
                if (r.Error == null)
                {
                    operationCompleted(r.Response.Data);
                }
                else
                {
                    operationCompleted(null);
                }
            });
        }

        public void DeleteGroup(int groupId, Action<Boolean> operationCompleted)
        {            
            _template.DeleteAsync("/api/groups/{groupId}",  r => 
            {
                if (r.Error == null)
                {
                    operationCompleted(true);
                }
                else
                {
                    operationCompleted(false);
                }
            }, groupId);
        }

        #endregion

        #region Follow/Unfollow user action 

        public void Follow(int userId,Action<Boolean> operatinCompleted)
        {
            IDictionary<string, object> parts = new Dictionary<string, object>();
            _template.PostForObjectAsync<ApiResponse<Boolean>>("/api/{userId}/follow",CreateEntity(), r => {
                if (r.Error == null)
                {
                    operatinCompleted(r.Response.Data);
                }
                else
                {
                    operatinCompleted(false);
                }
                
            },userId);
        }

        public void Unfollow(int userId, Action<Boolean> operationCompleted)
        {
            _template.DeleteAsync("/api/{userId}/follow", r => {
                if (r.Error == null)
                {
                    operationCompleted(true);
                }
                else
                {
                    operationCompleted(false);
                }
            }, userId);
        }
        #endregion
    
        #region Like/Unlike action

        public void LikePost(int postid, Action<Boolean> operationCompleted)
        {            
            _template.PostForObjectAsync<ApiResponse<Boolean>>("/api/{postId}/like",null, r => 
            {
                if (r.Error == null)
                {
                    operationCompleted(r.Response.Data);
                }
                else
                {
                    operationCompleted(false);
                }
            }, postid);
        }

        public void UnlikePost(int postId, Action<Boolean> operationCompleted)
        {            
            _template.DeleteAsync("/api/{postId}/like", r => 
            {
                if (r.Error == null)
                {
                    operationCompleted(true);
                }
                else
                {
                    operationCompleted(false);
                }
            }, postId);
        }
        #endregion

        #region Join / dissjoin a group actions
 
        public void Join(int groupId, Action<Boolean> operationCommpleted)
        {            
            _template.PostForObjectAsync<ApiResponse<Boolean>>("/api/{groupId}/join", null, r => 
            {
                if (r.Error == null)
                {
                    operationCommpleted(true);
                }
                else
                {
                    operationCommpleted(false);
                }
            }, groupId);
        }

        public void DissJoin(int groupId, Action<Boolean> operationCommpleted)
        {            
            _template.DeleteAsync("/api/{groupId}/follow", r =>
            {
                if (r.Error == null)
                {
                    operationCommpleted(true);
                }
                else
                {
                    operationCommpleted(false);
                }
            }, groupId);
        }

        #endregion

        #region Search action

        public void Search(string q, Action<ApiSearchResultViewModel> operationCompleted)
        {
            _template.GetForObjectAsync<ApiResponse<ApiSearchResultViewModel>>("/api/search?q={q}", r => 
            {
                if (r.Error == null)
                {
                    operationCompleted(r.Response.Data);
                }
                else 
                {
                    operationCompleted(null);
                }
            },q);
        }

        #endregion

        #region Profile 

        public void GetProfile(Action<UserProfile> operationCompleted)
        {
            HttpEntity entity = CreateEntity();
            _template.GetForObjectAsync<ApiResponse<UserProfile>>("/api/profile",  r =>
            {
                if (r.Error == null)
                {
                    operationCompleted(r.Response.Data);
                }
                else
                {
                    operationCompleted(null);
                }
            });
        }

        #endregion

        #region Users action

        public void GetFollowers(Action<List<UserProfile>, int, int, int> operationCompleted, int userId = int.MinValue, int page = 1, int size = 25)
        {
            _template.GetForObjectAsync<ApiResponse<List<UserProfile>>>("/api/followers/{userId}", r => {
                if (r.Error == null)
                {
                    int currentPage = r.Response.Meta.ContainsKey("currentPage") ? int.Parse(r.Response.Meta["currentPage"].ToString()) : 0;
                    int totalPage = r.Response.Meta.ContainsKey("totalPage") ? int.Parse(r.Response.Meta["totalPage"].ToString()) : 0;

                    operationCompleted(r.Response.Data, currentPage, totalPage, size);
                }
                else
                {
                    Debug.WriteLine(r.Error.ToString());
                    Debug.WriteLine(r.Response);
                    operationCompleted(new List<UserProfile>(), 0, 0, size);
                }
            },userId);
        }

        public void GetFollowees(Action<List<UserProfile>, int, int, int> operationCompleted, int userId = int.MinValue, int page = 1, int size = 25)
        {
            _template.GetForObjectAsync<ApiResponse<List<UserProfile>>>("/api/followees/{userId}", r =>
            {
                if (r.Error == null)
                {
                    int currentPage = r.Response.Meta.ContainsKey("currentPage") ? int.Parse(r.Response.Meta["currentPage"].ToString()) : 0;
                    int totalPage = r.Response.Meta.ContainsKey("totalPage") ? int.Parse(r.Response.Meta["totalPage"].ToString()) : 0;

                    operationCompleted(r.Response.Data, currentPage, totalPage, size);
                }
                else
                {
                    Debug.WriteLine(r.Error.ToString());
                    Debug.WriteLine(r.Response);
                    operationCompleted(new List<UserProfile>(), 0, 0, size);
                }
            },userId);
        }

        #endregion

        #region Private method

        private HttpEntity CreateEntity(object obj = null) 
        {
            HttpEntity entity;

            if (obj == null)
            {
                entity = new HttpEntity();
            }
            else 
            {
                entity = new HttpEntity(obj);
            }
            
            return entity;
        }

        #endregion
    }

    public class CoombuHeaderInterceptor : IClientHttpRequestBeforeInterceptor 
    {
        IDictionary<string, string> _headers;

        public CoombuHeaderInterceptor(IDictionary<string,string> headers)
        {
            this._headers = headers;
        }         

        public void BeforeExecute(IClientHttpRequestContext request)
        {
            foreach (var header in this._headers)
            {                
                request.Headers[header.Key] = header.Value;                                             
            }
            
            HttpMethod method = request.Method;

            if (method == HttpMethod.GET || method == HttpMethod.DELETE)
            {
                request.Headers.Remove("Content-Type");
            }   
        }
    }
}



