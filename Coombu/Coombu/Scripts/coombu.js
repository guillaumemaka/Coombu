$(function () {

    function resetBinding() {

        $("a.like").unbind();
        $("a.disslike").unbind();
        $("a.join").unbind();
        $("a.dissjoin").unbind();
        $("a.follow").unbind();
        $("a.unfollow").unbind();
        $("a.delete-post").unbind();
        $("a.delete-comment").unbind();

        $("a.like").bind("click", likeAction);
        $("a.disslike").bind("click", disslikeAction);
        $("a.join").bind("click",groupJoinAction);
        $("a.dissjoin").bind("click", groupDissjoinAction);
        $("a.follow").bind("click", followAction);
        $("a.unfollow").bind("click", unFollowAction);
        $("a.delete-post").bind("click", deletePost);
        $("a.delete-comment").bind("click", deleteComment);
    }


    function deletePost(event) {
        var $el = $(this);
              
        var postId = $el.attr("id");
        var $output = $(".output-" + postId);
        var data = { id: postId };
        var $notice = $("<span/>");
        var $selectedPost = $("#post-" + postId);

        $("<div  title='Supprimer'><p>Voulez vous vraiment supprimer ce post</p></div>").dialog({
            autoOpen: true,
            buttons: {
                "Oui": function () {
                    $.ajax("/posts/delete", {
                        data: data,
                        type: "DELETE",
                        dataType: "json",
                        success: function (data, status, jqXHR) {
                            if (data.Success) {
                                $selectedPost.fadeOut("slow");
                                $selectedPost.remove();
                            }

                            resetBinding();

                            return false;
                        }
                    });
                    $(this).dialog("close");
                },
                "Non": function () {
                    $(this).dialog("close");
                }
            }
        });

        return false;
    }

    function deleteComment(event) {
        var $el = $(this);
        var commentId = $el.attr("id");
        var $output = $(".output-" + commentId);
        var data = { id: commentId };
        var $notice = $("<span/>");
        var $selectedComment = $("#comment-" + commentId);

        $("<div title='Supprimer'><p>Voulez vous vraiment supprimer ce commentaire</p></div>").dialog({
            autoOpen: true,
            buttons: {
                "Oui": function () {
                    $.ajax("/comments/delete", {
                        data: data,
                        type: "DELETE",
                        dataType: "json",
                        success: function (data, status, jqXHR) {
                            if (data.Success) {
                                $selectedComment.fadeOut("slow");
                                $selectedComment.remove();
                            }

                            resetBinding();

                            return false;
                        }
                    });
                    $(this).dialog("close");
                },
                "Non": function () {
                    $(this).dialog("close");
                }
            }
        });        

        return false;
    }

    function likeAction(event) {        
        var $el = $(this);
        var postId = $el.attr("id");
        var $output = $(".output-" + postId);
        var data = { id: postId };
        var $notice = $("<span/>");
        var $badge = $("#badge-" + postId);

        $.post("/posts/like", data, function (resp) {

            if (resp.Success) {
                var message;

                if (resp.likes == 1) {
                    message = " personne aime ça";
                } else if (resp.likes > 1) {
                    message = " personnes aiment ça";
                } else {
                    message = ""
                }

                $el.removeClass("like").addClass("disslike");                
                $el.find("i").attr("class","icon-thumbs-down");
                $el.find("span").html("Je n'aime plus");
                $output.html(message);
                $badge.html((resp.likes == 0) ? "" : resp.likes);
            } else {
                $notice.insertAfter($output);
                $notice.html(resp.ErrorMessage);
                $notice.fadeOut("slow");
            }

            resetBinding();

            return false;
        }, "json");

        

        return false;
    }

    function disslikeAction(event) {        
        var $el = $(this);
        var postId = $el.attr("id");
        var $output = $(".output-" + postId);
        var data = { id: postId };
        var $notice = $("<span/>");
        var $badge = $("#badge-" + postId);

        $.ajax("/posts/like", {
            data: data,
            type: "DELETE",
            dataType : "json",
            success: function (data,status,jqXHR) {

                if (data.Success) {
                    var message;

                    if (data.likes == 1) {
                        message = " personne aime ça";
                    } else if (data.likes > 1) {
                        message = " personnes aiment ça";
                    } else {
                        message = ""
                    }

                    $el.removeClass("disslike").addClass("like");                
                    $el.find("i").attr("class","icon-thumbs-up");
                    $el.find("span").html("j'aime");
                    $el.bind("click", likeAction);
                    $output.html(message);
                    $badge.html((data.likes == 0) ? "" : data.likes);
                } else {
                    $notice.insertAfter($output);
                    $notice.html(resp.ErrorMessage);
                    $notice.fadeOut("slow");
                }

                resetBinding();

                return false;
            }
        });

        return false;
    }

    var groupJoinAction = function (event) {
        $el = $(this);
        data = { id: $el.attr("id") };

        $.post($el.attr("href"),data,function(resp){
            if (resp.Success) {
                $el.removeClass("btn-primary join")
                    .addClass("btn-success dissjoin")
                    .html("Membre");
                resetBinding();
            }
            return false;
        });
        return false;
    }

    var groupDissjoinAction = function (event) {
        $el = $(this);
        data = { id: $el.attr("id") };

        $.post($el.attr("href"), data, function (resp) {
            if (resp.Success) {
                $el.removeClass("btn-success dissjoin")
                    .addClass("btn-primary join")
                    .html("Rejoindre");
                resetBinding();
            }
            return false;
        });
        return false;
    }

    var followAction = function (event) {
        event.preventDefault();
        $el = $(this);
        data = { id: $el.attr("id") };

        $.post($el.attr("href"), data, function (resp) {
            $el.removeClass("btn-primary").addClass("btn-success");
            $el.html('Ne plus suivre');
            return false;
        });

        return false;
    }

    var unFollowAction = function (event) {
        event.preventDefault();
        $el = $(this);
        data = { id: $el.attr("id") };

        $.post($el.attr("href"), data, function (resp) {
            $el.removeClass("btn-success").addClass("btn-primary");
            $el.html('Suivre');
            return false;
        });

        return false;
    }

    $("form.comment").submit(function (event) {
        event.preventDefault();
        $commentContentInput = $("textarea[name=Content]", this);
        var data = {
            Content: $commentContentInput.val(),
            PostId: $("input[name=PostId]",this).val()
        };

        console.log(data);
        console.log($(this).attr("action"));        

        $template = $(
        '<div class="media">' +
            '<div class="btn-group pull-right">'+
                '<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><i class="icon-edit"></i></a>'+
                '<ul class="dropdown-menu">'+
                    '<li><a id="@comment.CommentId" data-toggle="modal" data-target="#deleteComment" href="#"><i class="icon-trash"></i> Supprimer</a></li>'+
                '</ul>'+
            '</div>'+
            '<span class="pull-left"> '+
                    '<img class="media-object" style="width: 64px; height: 64px;" src="/images/no_avatar.jpg" />'+
            '</span>'+
            '<div class="media-body">' +
                '<h4 class="media-heading"></h4>' +
                    '<div class="comment-content">' +
                    '</div>' +
            '</div>' +
        '</div>');

        $.post("/comments/create", data, function (resp) {
            if (resp.Success) {
                console.log(resp.Comment);
                $commentContentInput.val("");
                $template.find("h4.media-heading").html(resp.Comment.From);
                $template.find("div.comment-content").html(resp.Comment.Content);
                $("#post-" + resp.Comment.PostId + "-comments").append($template.fadeIn("slow"));                
            }
        }, "json");
        
        return false;
    });

    resetBinding();
});