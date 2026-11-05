import 'package:firstproj/SignupPage.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'data/UserService.dart'; //
import 'components/mytextfield.dart';
import 'components/mytextfield.dart';
import 'HomePage.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({super.key});

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  final GlobalKey<FormState> formKey = GlobalKey<FormState>();
  final TextEditingController usernamecontroller = TextEditingController();
  final TextEditingController passwordcontroller = TextEditingController();

  final UserService userService = UserService();
  bool isLoading = false;



  // دالة تسجيل الدخول
  void login() async {
    if (!formKey.currentState!.validate()) {
      return;
    }

    setState(() {
      isLoading = true;
    });

    try {
      // استخدام قيمة الـ Controllers الفعلية بعد إزالة الفراغات الزائدة
      final username = usernamecontroller.text.trim();
      final password = passwordcontroller.text;

      final user = await userService.login(username, password);

      if (!context.mounted) return;

      if (user != null) {
        Navigator.pushReplacement(
          context,
          MaterialPageRoute(
            builder: (context) => HomePage(
              username: user['username'] ?? 'User',
              userId: user['id'] ?? 0,
              email: user['email'] ?? 'user@email.com',
            ),
          ),
        );
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: const Text('Invalid username or password'),
            behavior: SnackBarBehavior.floating,
            duration: const Duration(seconds: 3),
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(20),
            ),
            backgroundColor: Colors.black38,
          ),
        );
      }
    } catch (e) {
      if (!context.mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Failed to connect to server'),
          backgroundColor: Colors.red,
        ),
      );
    } finally {
      if (mounted) {
        setState(() {
          isLoading = false; // إيقاف مؤشر التحميل في كلتا الحالتين
        });
      }
    }
  }
  @override
  void dispose() {
    usernamecontroller.dispose();
    passwordcontroller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('First App - Login Page'),
        backgroundColor: Colors.deepPurple,
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(20.0),
          child: Form(
            key: formKey, // ربط المفتاح بالـ Form لتفعيل الـ validation
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const SizedBox(height: 10),
                const Text(
                  'Welcome Back!',
                  style: TextStyle(
                    fontSize: 24,
                    color: Colors.purple,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 20),

                // حقل اسم المستخدم المخصص
                MyTextField(
                  label: 'Enter User Name',
                  controller: usernamecontroller,
                  hint: 'Username',
                  // validator: (value) {
                  //   if (value == null || value.trim().isEmpty) {
                  //     return 'Please enter your username';
                  //   }
                  //   return null;
                  // },
                ),
                const SizedBox(height: 20),

                // حقل كلمة المرور المخصص
                MyTextField(
                  label: 'Enter password',
                  controller: passwordcontroller,
                  hint: 'Password',
                  obscureText: true,
                  // validator: (value) {
                  //   if (value == null || value.isEmpty) {
                  //     return 'Please enter your password';
                  //   }
                  //   return null;
                  // },
                ),
                const SizedBox(height: 30),

                // زر تسجيل الدخول مع دعم حالة التحميل
                SizedBox(
                  width: double.infinity,
                  child: ElevatedButton(
                    onPressed: isLoading ? null : login,
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.purple,
                      padding: const EdgeInsets.symmetric(vertical: 15),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(20),
                      ),
                    ),
                    child: isLoading
                        ? const SizedBox(
                      height: 20,
                      width: 20,
                      child: CircularProgressIndicator(
                        color: Colors.white,
                        strokeWidth: 2,
                      ),
                    )
                        : const Text(
                      'Login',
                      style: TextStyle(
                        fontSize: 18,
                        color: Colors.white,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
                ),
                const SizedBox(height: 15),

                // الانتقال لصفحة إنشاء حساب
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    const Text("Don't have an account? "),
                    TextButton(
                      onPressed: () {
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (context) => const SignUpPage(),
                          ),
                        );
                      },
                      child: const Text(
                        'Sign Up',
                        style: TextStyle(
                          color: Colors.purple,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ),
      ),
      bottomNavigationBar: Container(
        color: Colors.deepPurple,
        height: 60,
      ),
    );
  }
}