import 'package:flutter/material.dart';

class MyTextField extends StatelessWidget {
  const MyTextField({
    super.key,
    required this.label,
    required this.hint,
    this.obscureText = false,
    this.controller, // 1. أضف المتحكم هنا
    this.validator,  // 2. أضف الفاليديشن هنا (اختياري)
  });

  final String label;
  final String hint;
  final bool obscureText;
  final TextEditingController? controller; // 3. تعريف المتغير
  final String? Function(String?)? validator; // تعريف دالة الفاليديشن

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          label,
          style: const TextStyle(
            fontSize: 20,
            color: Colors.purple,
            fontWeight: FontWeight.w700,
          ),
        ),
        const SizedBox(height: 8),
        TextFormField(
          controller: controller, // 4. ربطه بـ TextFormField
          obscureText: obscureText,
          validator: validator,   // ربطه بالفاليديشن
          decoration: InputDecoration(
            hintText: hint,
            enabledBorder: const OutlineInputBorder(
              borderSide: BorderSide(color: Colors.green, width: 1),
              borderRadius: BorderRadius.all(Radius.circular(20)),
            ),
            focusedBorder: const OutlineInputBorder(
              borderSide: BorderSide(color: Colors.green, width: 1),
              borderRadius: BorderRadius.all(Radius.circular(20)),
            ),
          ),
        ),
      ],
    );
  }
}