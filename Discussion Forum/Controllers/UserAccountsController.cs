using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Discussion_Forum.Data;
using Discussion_Forum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Discussion_Forum.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Discussion_Forum.Controllers
{
    public class UserAccountsController : Controller
    {
        private readonly DisscussionForumContext _context;
        private readonly UserManager<UserAccount> _userManager;
        private readonly SignInManager<UserAccount> _signInManager;
        public UserAccountsController(DisscussionForumContext context, SignInManager<UserAccount> signInManager,
            UserManager<UserAccount> usermanger)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = usermanger;
        }

        // GET: UserAccounts LogIn
        public IActionResult Index()
        {
            return View();
        }

        //POST :LogIn
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password,false,false);

                if (result.Succeeded)
                {
                    return RedirectToAction("home", "useraccounts");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }



        // GET: UserAccounts/SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: UserAccounts/SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Id,FullName,Email,Password")] SignUpViewModel UserInfo)
        {
            if (ModelState.IsValid)
            {
                UserAccount user = new UserAccount
                {
                    Email = UserInfo.Email,
                    FullName = UserInfo.FullName,
                    UserName = UserInfo.Email

                };
                var result = await _userManager.CreateAsync(user, UserInfo.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Home", "UserAccounts");
                }
                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(UserInfo);
        }


        //Get: Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View("Index");
        }

        // GET: UserAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }



        

        public IActionResult AnswerPost(int? id)
        {
            //Console.WriteLine("qid");
            //Console.WriteLine(qid);
            //ViewBag.questID = qid;
            ViewData["QuestID"] = id;
            return View();
        }

        public async Task<IActionResult> Home()
        {
            var id = _userManager.GetUserId(User);
            var disscussionForumContext = _context.Questions
                .Include(q => q.Topic)
                .Include(q => q.UserAccount);
            return View(await disscussionForumContext.ToListAsync());
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> AnswerView(int? id)
        {
            //var disscussionForumContext = _context.Answers.Include(a => a.Question).Include(a => a.UserAccount);
            //return View(await disscussionForumContext.ToListAsync());
            if (id == null)
            {
                return NotFound();
            }

            var answer = _context.Answers
                .Include(a => a.Question)
                .Include(a => a.UserAccount).Where(m => m.QuestId == id);
               // .FirstOrDefaultAsync(m => m.QuestId == id);
            if (answer == null)
            {
                return NotFound();
            }
            //return RedirectToAction("Answer");

            return View(await answer.ToListAsync());
        }

        // GET: PostedQuest/5
        public async Task<IActionResult> PostedQuest(string  id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question =  _context.Questions
                .Include(q => q.Topic)
                .Include(q => q.UserAccount)
                .Where(m => m.UserId.ToString() == id);
            // .FirstOrDefaultAsync(m => m.QuestId == id);
            if (question == null)
            {
                return NotFound();
            }
            //return RedirectToAction("Answer");

            return View(await question.ToListAsync());
        }


        //POST: LOGIn
        
        //[HttpPost]
        //public async Task<ActionResult> Login(UserAccount model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ////var userdetails = await _context.User
        //        ////.SingleOrDefaultAsync(m => m.EmailId == model.EmailId && m.Pass == model.Pass);
        //        //if (userdetails == null)
        //        //{
        //        //    ModelState.AddModelError("Password", "Invalid login attempt.");
        //        //    return View("Index");
        //        //}
        //        //HttpContext.Session.SetString("userName", userdetails.FullName);
        //        //HttpContext.Session.SetString("userID", userdetails.Id.ToString());
        //        //Console.WriteLine(userdetails.EmailId);


        //    }
        //    else
        //    {
        //        return View("Index");
        //    }
        //    return RedirectToAction("Home", "UserAccounts");
        //}

        

        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Answersubmit([Bind("Id,QuestId,UserId,Answers,AnsDate")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                answer.UserId = int.Parse(_userManager.GetUserId(User));
                answer.AnsDate= DateTime.Today;
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Home", "UserAccounts");
            }
            return View("Home");
        }
        // GET: UserAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //int id = int.Parse(uid);

            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.User.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }
            return View(userAccount);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,EmailId,Pass")] UserAccount userAccount)
        {
            //if (id != userAccount.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!UserAccountExists(userAccount.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction("Home");
            }
            return View(userAccount);
        }

        // GET: UserAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var userAccount = await _context.User
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (userAccount == null)
            //{
            //    return NotFound();
            //}

            return View();
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccount = await _context.User.FindAsync(id);
            _context.User.Remove(userAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool UserAccountExists(int id)
        //{
        //    return _context.User.Any(e => e.Id == id);
        //}

       

        [HttpGet]
        public ActionResult QuestAnsDetails(int? id)
        {
            //create object of ADO.Net Entity Data Model Code First  
            //var db = new QuestionAnswer();

            //Get Student detail as per student ID  
            var quest = _context.Questions
                .Include(q => q.Topic)
                .Include(q => q.UserAccount)
                .Where(m => m.QuestionId == id)
                .FirstOrDefault();
                
            //.Select(m=> new QuestionAnswerViewModel { 
            //Questions= new Question { },
            //Questtopic=m.Topic
            //} )


            //Get Result Exam marks detail as per student ID  
            var ans = _context.Answers
                .Include(q => q.Question)
                .Include(q => q.UserAccount)
                .Where(m => m.QuestId == id).ToList();

            //Output set to ViewModel  
            var model = new QuestionAnswerViewModel { Questions = quest, Answers = ans };

            //var model = new QuestionAnswerViewModel { Questions = quest, Questtopic=quest.Topic, Answers =ans };
            return View(model);
        }


        // POST: UserAccount/AddQuestion
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuestion([Bind("QuestionId,TopicId,UserId,Questions,CreatedDate")] Question question)
        {
            if (ModelState.IsValid)
            {
                //var uid = _userManager.GetUserId(User);
                question.UserId= int.Parse(_userManager.GetUserId(User));
                question.CreatedDate = DateTime.Today;
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction("home", "UserAccounts");
                //return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", question.TopicId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", question.UserId);
            //return View(question);
            return RedirectToAction("home", "UserAccounts");
        }
        public IActionResult AddQuestion()
        {
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "TopicName");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

    }
}
